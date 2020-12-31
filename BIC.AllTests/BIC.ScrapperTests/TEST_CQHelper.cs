﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.Utils;
using BIC.Scrappers.FinvizScrapper;
using BIC.Utils.Logger;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_CQHelper
    {
        private ILog _logger = LogServiceProvider.Logger;

        [TestMethod]
        public void TestCq()
        {
            Assert.IsTrue(UtilsForTesting.SetTheSettings(), "Settings initialization failed");

            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData("https://finviz.com/screener.ashx");
            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");
        }
        [TestMethod]
        public void TestCqWithParameters()
        {
            Assert.IsTrue(UtilsForTesting.SetTheSettings(), "Settings initialization failed");

            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData(generatedAddress);
            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");
        }
        [TestMethod]
        public void TestCqWithParametersFindTable()
        {
            Assert.IsTrue(UtilsForTesting.SetTheSettings(), "Settings initialization failed");

            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData(generatedAddress);
            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");

            _logger.Debug(cq.Find(@"table[bgcolor=""#d3d3d3""]").Contents().Text());
        }
    }
}