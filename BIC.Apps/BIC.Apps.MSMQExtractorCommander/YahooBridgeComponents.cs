﻿using BIC.Foundation.DataObjects;
using BIC.Scrappers.YahooScrapper;
using BIC.Utils.Logger;
using BIC.YahooScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    public class YahooBridgeComponents //: IBridgeComponents
    {
        private ILog _logger = LogServiceProvider.Logger;
        private string _sector;
        private string _tickerAfter;

        public YahooBridgeComponents(string sector, string tickerAfter)
        {
            _sector = sector;
            _tickerAfter = tickerAfter;
        }

        private IEnumerable<SecurityRecord> GenerateDataFilter()
        {
            bool skipTheStep = true;

            Func<string, IEnumerable<SectorRecord>> getSectorsOrSector = (s) =>
            {
                if (_sector != null && _sector != string.Empty)
                    return ETL.SqlServer.DataLayer.SecurityReader.GetSectors().Where(ss => ss.Sector == s);
                else
                    return ETL.SqlServer.DataLayer.SecurityReader.GetSectors();
            };

            foreach (var sector in getSectorsOrSector(_sector))
            {
                foreach (var security in ETL.SqlServer.DataLayer.SecurityReader.GetSecurities(sector.Sector))
                {
                    if (_tickerAfter != null && security.Ticker == _tickerAfter)
                    {
                        skipTheStep = false;
                        continue;
                    }

                    if (_tickerAfter != null && skipTheStep)
                        continue;
                    yield return security;
                }
            }
        }

        public void Scrap()
        {
            foreach(var security in GenerateDataFilter())
            {
                var oneShot = new OneShotScrapper();
                var yp = new YahooParameters() { Ticker = security.Ticker, ReportType = "balance-sheet" }; // It doesn't matter whether it is balance sheet, income or cashflow.
                _logger.Info($"Loading Ticker {security.Ticker} for report type {yp.ReportType} ..");
                bool result = oneShot.Scrap(yp);
                _logger.Info($"Finished Loading Ticker {security.Ticker} for report type {yp.ReportType}.");
            }
        }
    }
}
