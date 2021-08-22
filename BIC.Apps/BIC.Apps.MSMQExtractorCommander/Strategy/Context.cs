using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class Context
    {
        private IStrategy strategy;

        public IStrategy Strategy { get; set; }
        public void Do()
        {
            strategy.Execute();
        }
    }
}
