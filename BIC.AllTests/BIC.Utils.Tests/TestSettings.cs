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
            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.StringProperty);
            Assert.AreEqual(300,     s.IntegerProperty);
            Assert.AreEqual(Convert.ToDateTime("2000/12/31"), s.DateTimeProperty);
        }

        [TestMethod]
        public void TestPropertiesInitializationForOneLibraryGenericSetting()
        {
            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.GenericStringProperty);
        }

        [TestMethod]
        public void TestPropertiesInitializationForOneLibraryMandatorySetting()
        {
            var s = TestEntities.Settings.GetInstance();
            Assert.AreEqual("Value", s.StringMandatoryProperty);
        }

        [TestMethod]
        public void TestLogDirectoryProperty()
        {
            var s = BIC.Utils.Settings.GetInstance();
            Assert.AreEqual("E:\\_LOG", s.LogDirectory);
        }
    }
}
