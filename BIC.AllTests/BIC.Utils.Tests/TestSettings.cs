using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.Utils.Tests
{
    [TestClass]
    public class TestSettings
    {
        [TestMethod]
        public void TestPropertiesInitializationForOneLibrary()
        {
            var errors = BIC.Utils.Settings.AppSettingsProcessor.Populate();
            Assert.AreEqual(0, errors.Count);

            var s = TestEntities.Settings.GetInstance();
            Assert.IsNotNull(s.StringProperty);
            Assert.IsNotNull(s.IntegerProperty);
            Assert.IsNotNull(s.DateTimeProperty);
        }
    }
}
