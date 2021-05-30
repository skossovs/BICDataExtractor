using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
{
    /// <summary>
    /// in-Memory Command representation
    /// </summary>
    public class CommandCache
    {
        public CommandCache()
        {
            CommandLines = new List<CommandExe>();
        }
        public List<CommandExe> CommandLines;
    }
}
