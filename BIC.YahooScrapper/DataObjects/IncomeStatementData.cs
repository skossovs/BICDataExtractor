using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    public class IncomeStatementData : QuarterData
    {
        public QuarterNumber researchDevelopment               { get; set; }
        public QuarterNumber effectOfAccountingCharges         { get; set; }
        public QuarterNumber incomeBeforeTax                   { get; set; }
        public QuarterNumber minorityInterest                  { get; set; }
        public QuarterNumber netIncome                         { get; set; }
        public QuarterNumber sellingGeneralAdministrative      { get; set; }
        public QuarterNumber grossProfit                       { get; set; }
        public QuarterNumber ebit                              { get; set; }
        public QuarterNumber endDate                           { get; set; }
        public QuarterNumber operatingIncome                   { get; set; }
        public QuarterNumber otherOperatingExpenses            { get; set; }
        public QuarterNumber interestExpense                   { get; set; }
        public QuarterNumber extraordinaryItems                { get; set; }
        public QuarterNumber nonRecurring                      { get; set; }
        public QuarterNumber otherItems                        { get; set; }
        public QuarterNumber incomeTaxExpense                  { get; set; }
        public QuarterNumber totalRevenue                      { get; set; }
        public QuarterNumber totalOperatingExpenses            { get; set; }
        public QuarterNumber costOfRevenue                     { get; set; }
        public QuarterNumber totalOtherIncomeExpenseNet        { get; set; }
        public int maxAge                                      { get; set; }
        public QuarterNumber discontinuedOperations            { get; set; }
        public QuarterNumber netIncomeFromContinuingOps        { get; set; }
        public QuarterNumber netIncomeApplicableToCommonShares { get; set; }
    }

    public class IncomeStatementDataQuarterly : IncomeStatementData
    {
    }
}
