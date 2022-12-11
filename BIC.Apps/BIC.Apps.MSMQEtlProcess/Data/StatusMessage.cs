using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;

namespace BIC.Apps.MSMQEtlProcess.Data
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
