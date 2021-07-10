using BIC.Foundation.Interfaces;
using BIC.WPF.ScrapManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    public class ProcessStopMessage
    {
        public ProcessStopMessage(EProcessType processType)
        {
            ProcessType     = processType;
        }
        public EProcessType ProcessType { get; set; }
    }
}
