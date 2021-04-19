using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    public class ProcessStartMessage
    {
        public ProcessStartMessage(string processFilePath, string arguments)
        {
            ProcessFilePath = processFilePath;
            Arguments       = arguments;
        }
        public string ProcessFilePath { get; set; }
        public string Arguments       { get; set; }
    }
}
