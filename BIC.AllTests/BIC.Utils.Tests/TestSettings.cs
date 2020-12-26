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
            Assert.AreEqual("Value", s.StringProperty);
            Assert.AreEqual(300,     s.IntegerProperty);
            Assert.AreEqual(Convert.ToDateTime("2000/12/31"), s.DateTimeProperty);
        }

        [TestMethod]
        public void TestPropertiesInitializationForOneLibraryGenericSetting()
        {
            var errors = BIC.Utils.Settings.AppSettingsProcessor.Populate();
            Assert.AreEqual(0, errors.Count);

            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.GenericStringProperty);
        }
    }
}
