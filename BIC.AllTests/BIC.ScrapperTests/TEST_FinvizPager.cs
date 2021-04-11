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
            var fp = CreateFinvizParametersInstance(EView.Overview);
            var pager = new FinvizPager<FinvizParameters>();
            int recordsPerPage = 0;
            int maxPage = 0;
            bool result = pager.DefineMetrics(fp, out recordsPerPage, out maxPage);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAllPageScrapperOverviewData()
        {
            var allPageScrapper = new AllPageScrapper<OverviewData>();
            var fp = CreateFinvizParametersInstance(EView.Overview);
            allPageScrapper.Scrap(fp);
        }

        [TestMethod]
        public void TestAllPageScrapperFinancialData()
        {
            var allPageScrapper = new AllPageScrapper<FinancialData>();
            var fp = CreateFinvizParametersInstance(EView.Financial);
            allPageScrapper.Scrap(fp);

        }

        private FinvizParameters CreateFinvizParametersInstance(EView view)
        {
            return new FinvizParameters()
            {
                View = view,
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
