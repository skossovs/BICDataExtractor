using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Utils.MSMQ
{
    public class SenderReciever<TR,TS> : IDisposable
        where TR : Signal
        where TS : Signal
    {
        public readonly string SenderQueueName;    // TODO: //= ".\\Private$\\bic-commands"; //= ".\\Private$\\bic-status";
        public readonly string RecieverQueueName;
        public readonly int    ReadMessageWaitMSec = Settings.GetInstance().ReadMessageWaitMSec;

//        private Task                    _watchingTask;
        private CancellationTokenSource _tokenSource;

        private object                  _startStopLock;
        static EventWaitHandle          _waitHandle;

        public delegate void MessageRecievedHandler(TR body);
        public event         MessageRecievedHandler MessageRecievedEvent;

        public List<Exception> ExceptionLog;

        public SenderReciever(string senderQueueName, string recieverQueueName)
        {
            RecieverQueueName = recieverQueueName;
            SenderQueueName   = senderQueueName;
            // Threading
            _startStopLock    = new object();
            _waitHandle       = new AutoResetEvent(false);
            _waitHandle.Set();

            ExceptionLog = new List<Exception>();
        }

        public void Send(TS body)
        {
            if (!MessageQueue.Exists(SenderQueueName))
                throw new Exception("Queue doesn't exist: " + SenderQueueName);

            Message msg       = new Message() { Body = body };
            MessageQueue msgQ = new MessageQueue(SenderQueueName);

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

                if (!MessageQueue.Exists(RecieverQueueName))
                    throw new Exception("Queue doesn't exist: " + RecieverQueueName);

                MessageQueue msgQ = new MessageQueue(RecieverQueueName);
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
