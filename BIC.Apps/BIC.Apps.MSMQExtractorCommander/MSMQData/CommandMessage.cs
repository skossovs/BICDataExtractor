using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;

namespace BIC.Apps.MSMQExtractorCommander.MSMQData
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
