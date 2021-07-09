using BIC.Foundation.DataObjects;
using BIC.Scrappers.YahooScrapper;
using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.Utils.Logger;
using BIC.YahooScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.ExtractorCommander.Commands
{
    public static class Yahoo
    {
        private static ILog _logger = LogServiceProvider.Logger;

        public static void ScrapTickers()
        {
            foreach (var sector in ETL.SqlServer.DataLayer.SecurityReader.GetSectors())
            {
                ScrapTickers(sector.Sector);
            }
        }

        public static void ScrapTickers(string sector)
        {
            foreach (var security in ETL.SqlServer.DataLayer.SecurityReader.GetSecurities(sector))
            {
                ScrapOneShot(security);
            }
        }

        public static void ScrapTickersAfter(string ticker)
        {
            bool skipTheStep = true;
            foreach (var security in ETL.SqlServer.DataLayer.SecurityReader.GetSecurities())
            {
                if (security.Ticker == ticker)
                {
                    skipTheStep = false;
                    continue;
                }

                if (skipTheStep)
                    continue;

                ScrapOneShot(security);
            }
        }

        public static void ScrapTickersAfter(string sector, string ticker)
        {
            bool skipTheStep = true;
            foreach (var security in ETL.SqlServer.DataLayer.SecurityReader.GetSecurities(sector))
            {
                if (security.Ticker == ticker)
                {
                    skipTheStep = false;
                    continue;
                }

                if (skipTheStep)
                    continue;

                ScrapOneShot(security);
            }
        }

        private static void ScrapOneShot(SecurityRecord security)
        {
            var oneShot = new OneShotScrapper();
            var yp = new YahooParameters() { Ticker = security.Ticker, ReportType = "balance-sheet" }; // It doesn't matter whether it is balance sheet, income or cashflow.
            _logger.Info($"Loading Ticker {security.Ticker} for report type {yp.ReportType} ..");
            bool result = oneShot.Scrap(yp);
            _logger.Info($"Finished Loading Ticker {security.Ticker} for report type {yp.ReportType}.");
        }
// TODO: Drop it
        //private static void ScrapTrio(SecurityRecord security)
        //{
        //    if (!security.IsIncomeStatementQuarterly)
        //        Scrap<IncomeStatementDataQuarterly>(security.Ticker, true);
        //    if (!security.IsBalanceSheetQuarterly)
        //        Scrap<BalanceSheetDataQuarterly>(security.Ticker, true);
        //    if (!security.IsCashFlowQuarterly)
        //        Scrap<CashFlowDataQuarterly>(security.Ticker, true);
        //}

        //private static void Scrap<T>(string ticker, bool isQuarterly) where T : class, new()
        //{
        //    try
        //    {
        //        var yp = new YahooParameters()
        //        {
        //            IsQuarterly = isQuarterly,
        //            Ticker      = ticker
        //        };

        //        var typeName = typeof(T).Name;
        //        switch (typeName)
        //        {
        //            case "IncomeStatementDataQuarterly":
        //                yp.ReportType = "financials";
        //                break;
        //            case "BalanceSheetDataQuarterly":
        //                yp.ReportType = "balance-sheet";
        //                break;
        //            case "CashFlowDataQuarterly":
        //                yp.ReportType = "cash-flow";
        //                break;
        //            default:
        //                _logger.Error($"Unsupported type {typeName}");
        //                throw new Exception($"Unsupported type {typeName}");
        //        }

        //        _logger.Info($"Loading Ticker {ticker} for report type {yp.ReportType} ..");
        //        var pageScrapper = new PageScrapper<T>();
        //        var result = pageScrapper.Scrap(yp);
        //        _logger.Info($"Finished Loading Ticker {ticker} for report type {yp.ReportType}.");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.ReportException(ex);
        //    }
        //}
    }
}
