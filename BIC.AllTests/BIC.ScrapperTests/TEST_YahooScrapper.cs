using BIC.Scrappers.YahooScrapper;
using BIC.YahooScrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_YahooScrapper
    {
        [TestMethod]
        public void TestYahooIncomeStatementScrapperYearly()
        {
            var yp = CreateYahooParametersInstance("financials", "MSFT", false);
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestYahooIncomeStatementScrapperQuarterly()
        {
            var yp = CreateYahooParametersInstance("financials", "MSFT", true);
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestYahooBalanceSheetStatementScrapperYearly()
        {
            var yp = CreateYahooParametersInstance("balance-sheet", "MSFT", false);
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestYahooBalanceSheetStatementScrapperQuarterly()
        {
            var yp = CreateYahooParametersInstance("balance-sheet", "MSFT", true);
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestYahooCashFlowStatementScrapperYearly()
        {
            var yp = CreateYahooParametersInstance("cash-flow", "MSFT", false);
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestYahooOneShotScrapper()
        {
            var oneShot = new OneShotScrapper();
            var yp      = new YahooParameters() { Ticker = "MLR", ReportType = "balance-sheet" };
            bool result = oneShot.Scrap(yp);
            Assert.IsTrue(result);
        }

        private YahooParameters CreateYahooParametersInstance(string view, string ticker, bool isQuarterly)
        {
            return new YahooParameters()
            {
                ReportType  = view,
                Ticker      = ticker,
                IsQuarterly = isQuarterly
            };
        }
    }
}
