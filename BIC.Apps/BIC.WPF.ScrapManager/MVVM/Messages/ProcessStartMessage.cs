using BIC.WPF.ScrapManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    public class ProcessStartMessage
    {
        public ProcessStartMessage(string arguments, EProcessType processType)
        {
            Arguments       = arguments;
            ProcessType     = processType;
        }
        public string Arguments       { get; set; }

        public EProcessType ProcessType { get; set; }
    }
}
