using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
{
    public class CommandParameter
    {
        public CommandParameter()
        {
            DrillDownParameters = new List<CommandParameter>();
        }
        public string                 ParameterLine { get; set; }
        public List<CommandParameter> DrillDownParameters { get; set; }
    }
}
