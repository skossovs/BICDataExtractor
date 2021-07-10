using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
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
