using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
{
    public class CommandExe
    {
        public CommandExe()
        {
            CommandParameters = new List<CommandParameter>();
        }

        public string                 CommandLine { get; set; }
        public List<CommandParameter> CommandParameters { get; set; }
    }
}
