using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.FinvizScrapper;
using BIC.Scrappers.FinvizScrapper.DataObjects;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_FinvizPager
    {
        [TestMethod]
        public void TestDefineMetrics()
        {
            var fp = CreateFinvizParametersInstance();
            var pager = new FinvizPager<FinvizParameters>();
            int recordsPerPage = 0;
            int maxPage = 0;
            bool result = pager.DefineMetrics(fp, out recordsPerPage, out maxPage);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAllPageScrapper()
        {
            var allPageScrapper = new AllPageScrapper<OverviewData>();
            var fp = CreateFinvizParametersInstance();
            allPageScrapper.Scrap(fp);
        }

        private FinvizParameters CreateFinvizParametersInstance()
        {
            return new FinvizParameters()
            {
                View = EView.Overview,
                FilterView = EFilterView.All,
                Filters = new Filters()
                {
                    SectorFilter = "basicmaterials",
                    IndustryFilter = "gold",
                }
            };
        }
    }
}
