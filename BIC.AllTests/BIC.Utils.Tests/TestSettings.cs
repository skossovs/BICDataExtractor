﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.Utils.Tests
{
    [TestClass]
    public class TestSettings
    {
        [TestMethod]
        public void TestPropertiesInitializationForOneLibrary()
        {
            var settings = new SettingProcessors.AppSettingsProcessor();
            var result = settings.Populate();
            Assert.IsTrue(result);

            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.StringProperty);
            Assert.AreEqual(300,     s.IntegerProperty);
            Assert.AreEqual(Convert.ToDateTime("2000/12/31"), s.DateTimeProperty);
        }

        [TestMethod]
        public void TestPropertiesInitializationForOneLibraryGenericSetting()
        {
            var settings = new SettingProcessors.AppSettingsProcessor();
            var result = settings.Populate();
            Assert.IsTrue(result);

            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.GenericStringProperty);
        }

        [TestMethod]
        public void TestPropertiesInitializationForOneLibraryMandatorySetting()
        {
            var settings = new SettingProcessors.AppSettingsProcessor();
            var result = settings.Populate();
            Assert.IsTrue(result);

            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.StringMandatoryProperty);
        }

    }
}
