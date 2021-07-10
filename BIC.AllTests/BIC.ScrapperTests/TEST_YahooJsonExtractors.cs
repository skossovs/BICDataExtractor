using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.YahooScrapper.Chain;
using System.Collections.Generic;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_YahooJsonExtractors
    {
        private List<string> lst_json_content_lines;

        public TEST_YahooJsonExtractors()
        {
            var rawJsonData = System.IO.File.ReadAllText("./TestData/Yahoo_MLR.json");
            lst_json_content_lines = new List<string>() { rawJsonData };
        }

        [TestMethod]
        public void TestIncomeQuarterlyExtractor()
        {
            var extractor = new JsonObjectExtractor<IncomeStatementDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistoryQuarterly");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestBalanceQuarterlyExtractor()
        {
            var extractor = new JsonObjectExtractor<BalanceSheetDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistoryQuarterly");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestCashFlowQuarterlyExtractor()
        {
            var extractor = new JsonObjectExtractor<CashFlowDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistoryQuarterly");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIncomeYearlyExtractor()
        {
            var extractor = new JsonObjectExtractor<IncomeStatementData>("context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistory");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestBalanceYearlyExtractor()
        {
            var extractor = new JsonObjectExtractor<BalanceSheetData>("context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistory");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestCashFlowYearlyExtractor()
        {
            var extractor = new JsonObjectExtractor<CashFlowData>("context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistory");
            var result = RunTest(extractor, "MLR", true);
            Assert.IsTrue(result);
        }

        private bool RunTest(IActor extractor, string ticker, bool isQuarterly)
        {
            Context ctx = new Context()
            {
                Parameters = new Scrappers.YahooScrapper.YahooParameters()
                {
                    IsQuarterly = isQuarterly,
                    Ticker = ticker
                },
                JsonContentLines = lst_json_content_lines
            };
            return extractor.Do(ctx);
        }

    }
}
