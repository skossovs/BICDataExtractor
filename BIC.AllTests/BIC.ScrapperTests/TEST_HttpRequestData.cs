using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.FinvizScrapper;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_HttpRequestData
    {
        [TestInitialize]
        public void Init()
        {
            var settings = new BIC.Utils.Settings.AppSettingsProcessor(); // TODO: rename namespace Settings to something else
            var result = settings.Populate();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestFinvizDefaultOptions()
        {
            string expectedAddress = "https://finviz.com/screener.ashx";

            var generatedAddress = (new HttpRequestData()).GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }
    }
}
