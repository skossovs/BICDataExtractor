using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.Utils;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_CQHelper
    {
        [TestMethod]
        public void TestCq()
        {
            Assert.IsTrue(UtilsForTesting.SetTheSettings(), "Settings initialization failed");

            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData("https://finviz.com/screener.ashx");
            Assert.IsTrue(cq.Elements.Count() > 0, "Returns no elements");
        }

    }
}
