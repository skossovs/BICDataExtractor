using BIC.Scrappers.FinvizScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Logger;
using BIC.Apps.MSMQExtractorCommander.MSMQData;
using BIC.Utils.MSMQ;

namespace BIC.Apps.MSMQExtractorCommander
{
    public class StoppableStatusable : IStoppableStatusable
    {
        private bool _isStopped;
        private SenderReciever<CommandMessage, StatusMessage> _senderReceiver;
        // TODO: analyse the log and search for signals
        public bool IsStopped => _isStopped;

        public event StopperEventHandler Stopper;

        public StoppableStatusable(SenderReciever<CommandMessage, StatusMessage> senderReceiver)
        {
            _senderReceiver = senderReceiver;
            _senderReceiver.MessageRecievedEvent += ReceiveCommandMessage; // it's ok to attach from different objects to single source.
        }

        // intercept standard logger here
        public ILog OverrideLogger(ILog originalLogger)
        {
            return new UIStatusLogger(originalLogger, _senderReceiver);
        }

        private void ReceiveCommandMessage(CommandMessage cm)
        {
            if (cm.ProcessCommand == Foundation.Interfaces.EProcessCommand.Stop)
                _isStopped = true;
        }

        ~StoppableStatusable()
        {
            _senderReceiver.MessageRecievedEvent -= ReceiveCommandMessage;
        }
    }
}
