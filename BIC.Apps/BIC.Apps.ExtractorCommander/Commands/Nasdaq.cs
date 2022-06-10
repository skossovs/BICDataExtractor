using BIC.NasdaqLiveScrapper;
using BIC.NasdaqLiveScrapper.DataObjects;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.ExtractorCommander.Commands
{
    public static class Nasdaq
    {
        private static ILog _logger = LogServiceProvider.Logger;

        public static void Scrap()
        {
            _logger.Info($"Start Loading Nasdaq Data...");

            var nasdaqScrapper = new NasdaqScrapper<NasdaqData>();
            nasdaqScrapper.Scrap("AAPL");

            _logger.Info($"Finished Loading Nasdaq Data.");
        }

    }
}
