using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.FinvizScrapper;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_FinvizPager
    {
        [TestMethod]
        public void TestDefineMetrics()
        {
            var fp = new FinvizParameters()
            {
                View = EView.Overview,
                FilterView = EFilterView.All,
                Filters = new Filters()
                {
                    SectorFilter = "basicmaterials",
                    IndustryFilter = "gold",
                }
            };

            var pager = new FinvizPager<FinvizParameters>();
            int recordsPerPage = 0;
            int maxPage = 0;
            bool result = pager.DefineMetrics(fp, out recordsPerPage, out maxPage);
            Assert.IsTrue(result);
        }
    }
}
