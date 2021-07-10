using BIC.MoneyConverterScrapper;
using BIC.MoneyConverterScrapper.DataObjects;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.ExtractorCommander.Commands
{
    public static class FxRates
    {
        private static ILog _logger = LogServiceProvider.Logger;

        public static void Scrap()
        {
            _logger.Info($"Start Loading FX Data...");

            var fxScrapper = new FxScrapper<FxUsdData>();
            fxScrapper.Scrap();

            _logger.Info($"Finished Loading FX Data.");
        }
    }
}
