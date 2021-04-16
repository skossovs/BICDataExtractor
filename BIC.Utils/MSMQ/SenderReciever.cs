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

        private Task                    _watchingTask;
        private CancellationTokenSource _tokenSource;

        public delegate void MessageRecievedHandler(TR body);
        public event         MessageRecievedHandler MessageRecievedEvent;

        public SenderReciever(string senderQueueName, string recieverQueueName)
        {
            RecieverQueueName = recieverQueueName;
            SenderQueueName   = senderQueueName;
        }

        public void Send(TS body)
        {
            if (!MessageQueue.Exists(SenderQueueName))
                throw new Exception("Queue doesn't exist");

            Message msg       = new Message() { Body = body };
            MessageQueue msgQ = new MessageQueue(SenderQueueName);

            msgQ.Send(msg);
        }

        public void StartWatching()
        {
            if(_tokenSource == null)
            {
                _tokenSource         = new CancellationTokenSource();
                _watchingTask        = Task.Run(() => Watch(), _tokenSource.Token);
            }
        }

        public void StopWatching()
        {
            _tokenSource?.Cancel();

            do
            {
                Thread.Sleep(50);
            }
            while (_tokenSource != null);
        }

        public void Watch()
        {
            try
            {
                MessageQueue msgQ = new MessageQueue(RecieverQueueName);
                msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(TR) });
                CancellationToken ct = _tokenSource.Token;

                do
                {
                    if (msgQ.CanRead)
                    {
                        var body = (TR)msgQ.Receive().Body;
                        MessageRecievedEvent?.Invoke(body);
                    }
                    Thread.Sleep(200); // TODO: in Setup
                }
                while (!ct.IsCancellationRequested);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _tokenSource = null; // reset watching
            }
        }

        public void Dispose()
        {
            StopWatching();
        }
    }
}
