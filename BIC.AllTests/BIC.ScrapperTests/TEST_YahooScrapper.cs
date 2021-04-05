using BIC.Scrappers.YahooScrapper;
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
        public void TestYahooIncomeStatementScrapper()
        {
            var yp = CreateYahooParametersInstance("financials", "MSFT");
            var pager = new PageScrapper<YahooParameters>();
            bool result = pager.Scrap(yp);
            Assert.IsTrue(result);

        }

        private YahooParameters CreateYahooParametersInstance(string view, string ticker)
        {
            return new YahooParameters()
            {
                ReportType = view,
                Ticker     = ticker,
            };
        }
    }
}
