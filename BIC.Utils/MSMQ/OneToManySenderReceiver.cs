using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Utils.MSMQ
{

    public class OneToManySenderReceiver<TR, TS> : IDisposable
        where TR : Signal
        where TS : Signal
    {
        public readonly string                  ReceiverQueueName;
        public readonly Dictionary<int, string> SenderQueueNames;
        public readonly int                     ReadMessageWaitMSec;

        private static EventWaitHandle  _waitHandle;
        private CancellationTokenSource _tokenSource;
        private object                  _startStopLock;

        public delegate void MessageRecievedHandler(TR body);
        public event MessageRecievedHandler MessageRecievedEvent;
        public List<Exception> ExceptionLog;

        public OneToManySenderReceiver(int readMessageWaitMSec, Dictionary<int, string> senderQueueNames, string receiverQueueName)
        {
            ReceiverQueueName   = receiverQueueName;
            SenderQueueNames    = senderQueueNames;
            ReadMessageWaitMSec = readMessageWaitMSec;

            // Threading
            _startStopLock = new object();
            _waitHandle    = new AutoResetEvent(false);
            _waitHandle.Set();

            ExceptionLog = new List<Exception>();
        }

        public void Send(TS body)
        {
            var senderQueueName = SenderQueueNames[body.ChannelID];

            if (!MessageQueue.Exists(senderQueueName))
                throw new Exception("Queue doesn't exist: " + senderQueueName);

            Message msg = new Message() { Body = body };
            MessageQueue msgQ = new MessageQueue(senderQueueName);

            msgQ.Send(msg);
        }

        public void StartWatching()
        {
            lock (_startStopLock)
            {
                if (_tokenSource == null)
                {
                    _tokenSource = new CancellationTokenSource();
                    Task.Run(() => Watch(), _tokenSource.Token);
                }
            }
        }
        public void StopWatching()
        {
            lock (_startStopLock)
            {
                _tokenSource?.Cancel();
                _waitHandle?.WaitOne();
            }
        }

        public void Watch()
        {
            try
            {
                _waitHandle.Reset();

                if (!MessageQueue.Exists(ReceiverQueueName))
                    throw new Exception("Queue doesn't exist: " + ReceiverQueueName);

                MessageQueue msgQ = new MessageQueue(ReceiverQueueName);
                msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(TR) });
                CancellationToken ct = _tokenSource.Token;

                do
                {
                    if (msgQ.CanRead && msgQ.GetAllMessages().Length > 0)
                    {
                        var body = (TR)msgQ.Receive().Body;
                        MessageRecievedEvent?.Invoke(body);
                    }
                    Thread.Sleep(ReadMessageWaitMSec);
                }
                while (!ct.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                ExceptionLog.Add(ex);
            }
            finally
            {
                _waitHandle.Set();
            }
        }

        public void Dispose()
        {
            StopWatching();
        }
    }
}
