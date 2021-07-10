using BIC.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    public class ProcessStatusMessage
    {
        public ProcessStatusMessage(Foundation.Interfaces.EProcessStatus processStatus, EProcessType processType)
        {
            ProcessStatus = processStatus;
            ProcessType   = processType;
        }
        public EProcessStatus ProcessStatus { get; set; }
        public EProcessType   ProcessType { get; set; }
    }
}
