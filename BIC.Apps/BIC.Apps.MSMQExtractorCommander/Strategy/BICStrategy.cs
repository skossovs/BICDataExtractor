using BIC.Foundation.DataObjects;
using BIC.Scrappers.YahooScrapper;
using BIC.Utils.Logger;
using BIC.YahooScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class BICStrategy : IStrategy
    {
        private StrategyParameters _strategyParameters;
        private YahooBridgeComponents _yahooComponent;
        private static ILog _logger = LogServiceProvider.Logger;

        public BICStrategy(StrategyParameters strategyParameters)
        {
            _strategyParameters = strategyParameters;
            _yahooComponent = new YahooBridgeComponents(strategyParameters.Sector, strategyParameters.TickerAt);
        }

        public void Execute()
        {
            _logger.Info($"Start Loading BIC Data...");
            _yahooComponent.Scrap();
            _logger.Info($"Finished Loading BIC Data.");
        }
    }
}
