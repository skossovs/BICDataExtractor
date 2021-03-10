using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.FinvizScrapper;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_HttpRequestData
    {
        [TestMethod]
        public void TestFinvizDefaultOptions()
        {
            string expectedAddress = "https://finviz.com/screener.ashx";
            var generatedAddress = (new HttpRequestData()).GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }

        [TestMethod]
        public void TestFinvizAllFiveOptions()
        {
            string expectedAddress = "https://finviz.com/screener.ashx?v=111&f=exch_nyse,geo_usa,idx_sp500,ind_gold,sec_basicmaterials&ft=4";
            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Exchange = "nyse", Index = "sp500", Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }


        [TestMethod]
        public void TestFinvizAllFiveOptionsWithParametersClass()
        {
            string expectedAddress = "https://finviz.com/screener.ashx?v=111&f=exch_nyse,geo_usa,idx_sp500,ind_gold,sec_basicmaterials&ft=4";

            var fp = new FinvizParameters()
            {
                View       = EView.Overview,
                FilterView = EFilterView.All,
                Filters    = new Filters()
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
    }
}
