using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.ETL.SqlServer.FileReaders;

namespace BIC.ETL.SqlServerTest
{
    [TestClass]
    public class TestMergers
    {
        // TODO: this test has file dependency
        [TestMethod]
        public void TestFinvizKeyRatioFileMerger()
        {
            var merger = new FinvizKeyRatioFileMerger(@"E:\_DATA\BIC.FinvizScrapper_FinancialData_3-21-2021 1-18-44 PM.json", new DateTime(2021, 3, 21));
            var data   = merger.Read();
            merger.Merge(data);
        }
    }
}
