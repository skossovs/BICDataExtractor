using BIC.Scrappers.YahooScrapper;
using BIC.Scrappers.YahooScrapper.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.YahooScrapper.Chain
{
    public class ChainFactory
    {
        public static IActor CreateInstance()
        {
            IActor current = new JsonObjectScrapper();
            IActor root    = current;
            current.Next   = new JsonObjectExtractor<IncomeStatementDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistoryQuarterly");
            current        = current.Next;
            current.Next   = new JsonObjectExtractor<BalanceSheetDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistoryQuarterly");
            current        = current.Next;
            current.Next   = new JsonObjectExtractor<CashFlowDataQuarterly>("context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistoryQuarterly");
            current        = current.Next;
            current.Next   = new JsonObjectExtractor<IncomeStatementData>("context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistory");
            current        = current.Next;
            current.Next   = new JsonObjectExtractor<BalanceSheetData>("context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistory");
            current        = current.Next;
            current.Next   = new JsonObjectExtractor<CashFlowData>("context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistory");
            return root;
        }
    }
}
