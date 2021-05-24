using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.MoneyConverterScrapper;
using BIC.MoneyConverterScrapper.DataObjects;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_FxScrappers
    {

        // https://themoneyconverter.com/#exchange-rates
        [TestMethod]
        public void TestMoneyConverterSite()
        {
            var fxScrapper = new FxScrapper<FxUsdData>();
            bool result = fxScrapper.Scrap();
            Assert.IsTrue(result);
        }
    }
}
