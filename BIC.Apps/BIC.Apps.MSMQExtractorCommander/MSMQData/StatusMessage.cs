using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;

namespace BIC.Apps.MSMQExtractorCommander.MSMQData
{
    public class StatusMessage : Signal, IStatusMessage
    {
        public EProcessStatus ProcessStatus
        {
            get;
            set;
        }
    }
}
