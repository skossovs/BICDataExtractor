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
        public void TestCqFinvizSite()
        {
            var delayer                = new BIC.Scrappers.Utils.Delayers.VariableDelayer();
            var chromeRetriever        = new ChromeRetriever(delayer);
            var currentPagehtmlContent = chromeRetriever.GetData("https://finviz.com/screener.ashx");
            var cqHelper               = new CQHelper();
            var cq                     = cqHelper.InitiateWithContent(currentPagehtmlContent);

            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");
        }
        [TestMethod]
        public void TestCqWithParameters()
        {
            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            var chromeRetriever = new ChromeRetriever(new Scrappers.Utils.Delayers.VariableDelayer());
            var currentPagehtmlContent = chromeRetriever.GetData(generatedAddress);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");
        }
        [TestMethod]
        public void TestCqWithParametersFindTableOnTheFirstPage()
        {
            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Sector = "basicmaterials", Industry = "gold", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            var chromeRetriever = new ChromeRetriever(new Scrappers.Utils.Delayers.VariableDelayer());
            var currentPagehtmlContent = chromeRetriever.GetData(generatedAddress);

            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");

            cq = cq.Find(@"table[bgcolor=""#d3d3d3""]");

            // Find Headers
            var cqHeaders = cq.Find(@"td[class=""table-top""]");
            // Display headers
            foreach (var h in cqHeaders)
            {
                _logger.Debug(h.InnerHTML);
            }

            // Find Rows & Cells
            var cqRows = cq.Find(@"tr[class$=""-row-cp""]"); // ends with -row-cp
            foreach (var dRow in cqRows.Contents())
            {
                // Extract Cells
                var cqCells = cqHelper.InitiateWithContent(dRow.Render());
                _logger.Debug(cqCells.Contents().Text());
            }
        }

        [TestMethod]
        public void TestFindingPageMetricsFragment()
        {
            var r = new HttpRequestData();
            r.View = "111";
            r.FilterView = "4";
            r.Filters = new HttpRequestData.Filter() { Sector = "basicmaterials", Country = "usa" };
            var generatedAddress = r.GenerateAddressRequest();

            var chromeRetriever = new ChromeRetriever(new Scrappers.Utils.Delayers.VariableDelayer());
            var allContent = chromeRetriever.GetData(generatedAddress);

            Assert.IsTrue(allContent.Contains("Page 5"));
        }
    }
}
