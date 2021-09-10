using BIC.Scrappers.FinvizScrapper;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class KeyRatiosStrategy : IStrategy
    {
        private StrategyParameters _strategyParameters;
        private IBridgeComponents _finvizComponent;
        private IStoppableStatusable _stoppableStatusable;

        public KeyRatiosStrategy(StrategyParameters strategyParameters, IStoppableStatusable stoppableStatusable)
        {
            _strategyParameters = strategyParameters;
            _stoppableStatusable = stoppableStatusable;
        }

        public void Execute()
        {
            _finvizComponent = new FinvizBridgeComponents(_strategyParameters.Sector, _stoppableStatusable);
            _finvizComponent.Scrap<FinancialData>();
        }
    }
}
