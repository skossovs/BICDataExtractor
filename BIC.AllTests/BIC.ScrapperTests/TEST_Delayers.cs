using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_Delayers
    {

        [TestMethod]
        public void TestVariableDelay()
        {
            var delayer = new BIC.Scrappers.Utils.Delayers.VariableDelayer();
            var t1 = DateTime.Now;
            delayer.Wait();
            var t2 = DateTime.Now;
            delayer.Wait();
            var t3 = DateTime.Now;
            delayer.Wait();
            var t4 = DateTime.Now;

            var dt1 = t2 - t1;
            var dt2 = t3 - t2;
            var dt3 = t4 - t3;

            Assert.AreNotEqual(dt1, dt2);
            Assert.AreNotEqual(dt2, dt3);
        }
    }
}
