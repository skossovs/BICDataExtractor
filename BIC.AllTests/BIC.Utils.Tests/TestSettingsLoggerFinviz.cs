using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.Utils.Tests
{
    [TestClass]
    public class TestSettingsLoggerFinviz
    {
        [TestMethod]
        public void TestPropertiesInitializationForFinvizLibrary()
        {
            var s = BIC.Scrappers.FinvizScrapper.Settings.GetInstance();
            Assert.AreEqual("https://finviz.com/screener.ashx", s.UrlRoot);
        }
    }
}
