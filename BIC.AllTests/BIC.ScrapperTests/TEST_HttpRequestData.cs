using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.FinvizScrapper;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_HttpRequestData
    {
        public void SetTheSettings()
        {
            var settings = new Utils.SettingProcessors.AppSettingsProcessor();
            var result = settings.Populate();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestFinvizDefaultOptions()
        {
            SetTheSettings();

            string expectedAddress = "https://finviz.com/screener.ashx";
            var generatedAddress = (new HttpRequestData()).GenerateAddressRequest();

            Assert.AreEqual(generatedAddress, expectedAddress);
        }
    }
}
