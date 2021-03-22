using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIC.Utils.Tests
{
    [TestClass]
    public class TestConversions
    {
        [TestMethod]
        public void TestQuarters()
        {
            Tuple<int, int>[] quarterTuples = new Tuple<int, int>[]   {
            Conversions.DateToYearQuarter(new DateTime(2021, 1, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 2, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 3, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 4, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 5, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 6, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 7, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 8, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 9, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 10, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 11, 1)),
            Conversions.DateToYearQuarter(new DateTime(2021, 12, 1))   };

            Assert.AreEqual(new Tuple<int, int>(2021, 1), quarterTuples[0]);
            Assert.AreEqual(new Tuple<int, int>(2021, 1), quarterTuples[1]);
            Assert.AreEqual(new Tuple<int, int>(2021, 1), quarterTuples[2]);
            Assert.AreEqual(new Tuple<int, int>(2021, 2), quarterTuples[3]);
            Assert.AreEqual(new Tuple<int, int>(2021, 2), quarterTuples[4]);
            Assert.AreEqual(new Tuple<int, int>(2021, 2), quarterTuples[5]);
            Assert.AreEqual(new Tuple<int, int>(2021, 3), quarterTuples[6]);
            Assert.AreEqual(new Tuple<int, int>(2021, 3), quarterTuples[7]);
            Assert.AreEqual(new Tuple<int, int>(2021, 3), quarterTuples[8]);
            Assert.AreEqual(new Tuple<int, int>(2021, 4), quarterTuples[9]);
            Assert.AreEqual(new Tuple<int, int>(2021, 4), quarterTuples[10]);
            Assert.AreEqual(new Tuple<int, int>(2021, 4), quarterTuples[11]);
        }
    }
}
