using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    public class IncomeStatementData : YahooFinanceData
    {
        public YahooFinanceNumber researchDevelopment               { get; set; }
        public YahooFinanceNumber effectOfAccountingCharges         { get; set; }
        public YahooFinanceNumber incomeBeforeTax                   { get; set; }
        public YahooFinanceNumber minorityInterest                  { get; set; }
        public YahooFinanceNumber netIncome                         { get; set; }
        public YahooFinanceNumber sellingGeneralAdministrative      { get; set; }
        public YahooFinanceNumber grossProfit                       { get; set; }
        public YahooFinanceNumber ebit                              { get; set; }
        public YahooFinanceNumber endDate                           { get; set; }
        public YahooFinanceNumber operatingIncome                   { get; set; }
        public YahooFinanceNumber otherOperatingExpenses            { get; set; }
        public YahooFinanceNumber interestExpense                   { get; set; }
        public YahooFinanceNumber extraordinaryItems                { get; set; }
        public YahooFinanceNumber nonRecurring                      { get; set; }
        public YahooFinanceNumber otherItems                        { get; set; }
        public YahooFinanceNumber incomeTaxExpense                  { get; set; }
        public YahooFinanceNumber totalRevenue                      { get; set; }
        public YahooFinanceNumber totalOperatingExpenses            { get; set; }
        public YahooFinanceNumber costOfRevenue                     { get; set; }
        public YahooFinanceNumber totalOtherIncomeExpenseNet        { get; set; }
        public int maxAge                                      { get; set; }
        public YahooFinanceNumber discontinuedOperations            { get; set; }
        public YahooFinanceNumber netIncomeFromContinuingOps        { get; set; }
        public YahooFinanceNumber netIncomeApplicableToCommonShares { get; set; }
    }

    public class IncomeStatementDataQuarterly : IncomeStatementData
    {
    }
}
