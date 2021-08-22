using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class SecMasterStrategy : IStrategy
    {
        private StrategyParameters _strategyParameters;
        public SecMasterStrategy(StrategyParameters strategyParameters)
        {
            _strategyParameters = strategyParameters;
        }
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
