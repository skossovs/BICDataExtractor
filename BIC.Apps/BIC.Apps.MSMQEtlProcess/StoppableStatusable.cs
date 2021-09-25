using BIC.Apps.MSMQEtlProcess.Data;
using BIC.Foundation.Interfaces;
using BIC.Utils.Logger;
using BIC.Utils.MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQEtlProcess
{
    // TODO: let's see if this class diverge from Extractor's, refactor later if not.
    public class StoppableStatusable : IStoppableStatusable<ILog>
    {
        private bool _isStopped;
        private SenderReciever<CommandMessage, StatusMessage> _senderReceiver;
        // analyse the log and search for signals
        public bool IsStopped => _isStopped;

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
