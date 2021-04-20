using System;
using BIC.Foundation.Interfaces;
using BIC.Utils.MSMQ;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
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
