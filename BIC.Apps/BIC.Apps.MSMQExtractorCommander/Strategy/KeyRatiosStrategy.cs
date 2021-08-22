using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class KeyRatiosStrategy : IStrategy
    {
        private StrategyParameters _strategyParameters;
        public KeyRatiosStrategy(StrategyParameters strategyParameters)
        {
            _strategyParameters = strategyParameters;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
