using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    public class CashFlowData : YahooFinanceData
    {
        public YahooFinanceNumber investments                            { get; set; }
        public YahooFinanceNumber changeToLiabilities                    { get; set; }
        public YahooFinanceNumber totalCashflowsFromInvestingActivities  { get; set; }
        public YahooFinanceNumber netBorrowings                          { get; set; }
        public YahooFinanceNumber totalCashFromFinancingActivities       { get; set; }
        public YahooFinanceNumber changeToOperatingActivities            { get; set; }
        public YahooFinanceNumber issuanceOfStock                        { get; set; }
        public YahooFinanceNumber netIncome                              { get; set; }
        public YahooFinanceNumber changeInCash                           { get; set; }
        public YahooFinanceNumber endDate                                { get; set; }
        public YahooFinanceNumber repurchaseOfStock                      { get; set; }
        public YahooFinanceNumber effectOfExchangeRate                   { get; set; }
        public YahooFinanceNumber totalCashFromOperatingActivities       { get; set; }
        public YahooFinanceNumber depreciation                           { get; set; }
        public YahooFinanceNumber otherCashflowsFromInvestingActivities  { get; set; }
        public YahooFinanceNumber dividendsPaid                          { get; set; }
        public YahooFinanceNumber changeToInventory                      { get; set; }
        public YahooFinanceNumber changeToAccountReceivables             { get; set; }
        public YahooFinanceNumber otherCashflowsFromFinancingActivities  { get; set; }
        public int maxAge                                           { get; set; }
        public YahooFinanceNumber changeToNetincome                      { get; set; }
        public YahooFinanceNumber capitalExpenditures                    { get; set; }
    }

    public class CashFlowDataQuarterly : CashFlowData
    {
    }
}
