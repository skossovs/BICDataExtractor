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
        public void TestExtractor()
        {
/*
                jsonPath = "context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistoryQuarterly";
                jsonPath = "context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistoryQuarterly";
                jsonPath = "context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistoryQuarterly";
*/

            var extractor = new JsonObjectExtractor<IncomeStatementDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistoryQuarterly");

            Context ctx = new Context()
            {
                Parameters = new Scrappers.YahooScrapper.YahooParameters()
                {
                    IsQuarterly = true,
                    Ticker = "MLR"
                },
                JsonContentLines = lst_json_content_lines
            };
            extractor.Do(ctx);
        }
    }
}
