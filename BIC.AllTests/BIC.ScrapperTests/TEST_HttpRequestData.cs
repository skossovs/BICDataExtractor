using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_HttpRequestData
    {
        [TestMethod]
        public void TestFinvizDefaultOptions()
        {
            string expectedAddress = "https://finviz.com/screener.ashx";
            var generatedAddress = (new Scrappers.FinvizScrapper.HttpRequestData()).GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }

        [TestMethod]
        public void TestFinvizAllFiveOptions()
        {
            string expectedAddress = "https://finviz.com/screener.ashx?v=111&f=exch_nyse,geo_usa,idx_sp500,ind_gold,sec_basicmaterials&ft=4";
            var r = new Scrappers.FinvizScrapper.HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new Scrappers.FinvizScrapper.HttpRequestData.Filter() { Exchange = "nyse", Index = "sp500", Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }


        [TestMethod]
        public void TestFinvizAllFiveOptionsWithParametersClass()
        {
            string expectedAddress = "https://finviz.com/screener.ashx?v=111&f=exch_nyse,geo_usa,idx_sp500,ind_gold,sec_basicmaterials&ft=4";

            var fp = new Scrappers.FinvizScrapper.FinvizParameters()
            {
                View       = Scrappers.FinvizScrapper.EView.Overview,
                FilterView = Scrappers.FinvizScrapper.EFilterView.All,
                Filters    = new Scrappers.FinvizScrapper.Filters()
                {
                    CountryFilter   = "usa",
                    ExchangeFilter = "nyse",
                    IndexFilter    = "sp500",
                    SectorFilter   = "basicmaterials",
                    IndustryFilter = "gold",
                }
            };

            var r = BIC.Scrappers.FinvizScrapper.Conversions.FromFinvizParametersToHttpRequestData(fp);
            var generatedAddress = r.GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }

        [TestMethod]
        public void TestYahooIncomeStatement()
        {
            string expectedAddress = "https://finance.yahoo.com/quote/V/balance-sheet?p=V";

            var fp = new Scrappers.YahooScrapper.YahooParameters() { Ticker = "V"      , ReportType = "balance-sheet" };
            var r  = new Scrappers.YahooScrapper.HttpRequestData() { Ticker = fp.Ticker, ReportType = fp.ReportType   };
            var generatedAddress = r.GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }
    }
}
