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

        public KeyRatiosStrategy(StrategyParameters strategyParameters)
        {
            _strategyParameters = strategyParameters;
        }

        public void Execute()
        {
            _finvizComponent = new FinvizBridgeComponents(_strategyParameters.Sector);
            _finvizComponent.Scrap<FinancialData>();
        }
    }
}
