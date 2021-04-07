using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    public class CashFlowData
    {
    }

    public class CashFlowDataQuarterly : QuarterData
    {
        public QuarterNumber investments                            { get; set; }
        public QuarterNumber changeToLiabilities                    { get; set; }
        public QuarterNumber totalCashflowsFromInvestingActivities  { get; set; }
        public QuarterNumber netBorrowings                          { get; set; }
        public QuarterNumber totalCashFromFinancingActivities       { get; set; }
        public QuarterNumber changeToOperatingActivities            { get; set; }
        public QuarterNumber issuanceOfStock                        { get; set; }
        public QuarterNumber netIncome                              { get; set; }
        public QuarterNumber changeInCash                           { get; set; }
        public QuarterNumber endDate                                { get; set; }
        public QuarterNumber repurchaseOfStock                      { get; set; }
        public QuarterNumber effectOfExchangeRate                   { get; set; }
        public QuarterNumber totalCashFromOperatingActivities       { get; set; }
        public QuarterNumber depreciation                           { get; set; }
        public QuarterNumber otherCashflowsFromInvestingActivities  { get; set; }
        public QuarterNumber dividendsPaid                          { get; set; }
        public QuarterNumber changeToInventory                      { get; set; }
        public QuarterNumber changeToAccountReceivables             { get; set; }
        public QuarterNumber otherCashflowsFromFinancingActivities  { get; set; }
        public int maxAge                                           { get; set; }
        public QuarterNumber changeToNetincome                      { get; set; }
        public QuarterNumber capitalExpenditures                    { get; set; }
    }
}
