using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    public class ProcessStopMessage
    {
        public ProcessStopMessage(string processFilePath)
        {
            ProcessFilePath = processFilePath;
        }
        public string ProcessFilePath { get; set; }
    }
}
