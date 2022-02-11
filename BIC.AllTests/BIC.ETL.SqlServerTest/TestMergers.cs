using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.ETL.SqlServer.FileReaders;

namespace BIC.ETL.SqlServerTest
{
    [TestClass]
    public class TestMergers
    {
        // https://github.com/linq2db/linq2db start using linq2db for merge operation
        [TestMethod]
        public void TestFinvizKeyRatioFileMerger()
        {
            var merger = new FinvizKeyRatioFileMerger(@"TestFiles\BIC.FinvizScrapper_FinancialData_3-21-2021 1-18-44 PM.json", new DateTime(2021, 3, 21));
            var data   = merger.Read();
            merger.Merge(data);
        }

        [TestMethod]
        public void TestYahooBalanceSheetQuarterlyMerger()
        {
            var merger = new YahooBalanceSheetQuarterlyMerger(@"TestFiles\BIC.YahooScrapper_BalanceSheetDataQuarterly_4-6-2021 11-54-26 PM.json");
            var data = merger.Read();
            merger.Merge(data);
        }

        [TestMethod]
        public void TestYahooIncomeStatementQuarterlyMerger()
        {
            var merger = new YahooIncomeStatementQuarterlyMerger(@"TestFiles\BIC.YahooScrapper_IncomeStatementDataQuarterly_4-6-2021 11-55-15 PM.json");
            var data = merger.Read();
            merger.Merge(data);
        }

        [TestMethod]
        public void TestYahooCashFlowQuarterlyMerger()
        {
            var merger = new YahooCashFlowQuarterlyMerger(@"TestFiles\BIC.YahooScrapper_CashFlowDataQuarterly_4-6-2021 11-54-43 PM.json");
            var data = merger.Read();
            merger.Merge(data);
        }

        [TestMethod]
        public void TestMergeErrorOnQuarterlyBalanceSheet()
        {
            var merger = new YahooBalanceSheetQuarterlyMerger(@"TestFiles\BIC.YahooScrapper_BalanceSheetData_2-4-2022 1-27-33 PM.json");
            var data = merger.Read();
            merger.Merge(data);
        }
    }
}
