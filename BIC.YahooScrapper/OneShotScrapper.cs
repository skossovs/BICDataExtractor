using BIC.Scrappers.YahooScrapper;
using BIC.Utils.Logger;
using BIC.YahooScrapper.Chain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.YahooScrapper
{
    public class OneShotScrapper
    {
        private ILog   _logger = LogServiceProvider.Logger;
        private IActor _actor;

        public OneShotScrapper()
        {
            _actor = ChainFactory.CreateInstance();
        }
        public bool Scrap(YahooParameters requestParameters)
        {
            var ctx = new Context() { Parameters = requestParameters };
            return _actor.Do(ctx);
        }
    }
}
