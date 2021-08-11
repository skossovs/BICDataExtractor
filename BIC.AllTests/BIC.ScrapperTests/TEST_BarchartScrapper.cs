using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.BarchartScrapper;
using BIC.BarchartScrapper.DataObjects;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_BarchartScrapper
    {
        [TestMethod]
        public void TestBarchartBullishSite()
        {
            var Scrapper = new BarchartScrapper<BarchartData>();
            bool result = Scrapper.Scrap();
            Assert.IsTrue(result);
        }
    }
}
