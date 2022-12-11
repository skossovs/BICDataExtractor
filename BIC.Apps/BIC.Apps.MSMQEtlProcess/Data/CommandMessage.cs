using System;
using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;

namespace BIC.Apps.MSMQEtlProcess.Data
{
    public class CommandMessage : Signal, ICommandMessage
    {
        public EProcessCommand ProcessCommand
        {
            get;
            set;
        }
    }
}
