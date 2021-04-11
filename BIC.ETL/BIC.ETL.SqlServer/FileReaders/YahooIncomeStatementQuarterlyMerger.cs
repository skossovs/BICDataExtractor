using System;
using BIC.Scrappers.YahooScrapper.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Logger;
using BIC.Utils;
using BIC.ETL.SqlServer.DataLayer;
using LinqToDB;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class YahooIncomeStatementQuarterlyMerger : FileReader<IncomeStatementDataQuarterly>, IFileMerger<IncomeStatementDataQuarterly>
    {
        private ILog _logger = LogServiceProvider.Logger;
        public YahooIncomeStatementQuarterlyMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<IncomeStatementDataQuarterly> newData)
        {
            // TODO: Validate for Late fillings:
            //var qDates = from f in newData
            //             select Convert.ToDateTime(f.endDate.fmt).DateToYearQuarter();

            var db = DataConnectionFactory.CreateInstance();
            var qNewData = from y in newData
                           join s in db.Securities.Select(s1 => new { s1.SecurityID, s1.Ticker }) on y.Ticker equals s.Ticker
                           select new IncomeStatementQuarterly
                           {
                               SecurityID = s.SecurityID,
                               Year       = Convert.ToDateTime(y.endDate.fmt).DateToYearQuarter().Item1,
                               Quarter    = Convert.ToDateTime(y.endDate.fmt).DateToYearQuarter().Item2,
                               EndDate    = Convert.ToDateTime(y.endDate.fmt),
                               TotalRevenue                      = y.totalRevenue?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               CostOfRevenue                     = y.costOfRevenue?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ResearchDevelopment               = y.researchDevelopment?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalOperatingExpenses            = y.totalOperatingExpenses?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalOtherIncomeExpenseNet        = y.totalOtherIncomeExpenseNet?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherOperatingExpenses            = y.otherOperatingExpenses?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               MinorityInterest                  = y.minorityInterest?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               InterestExpense                   = y.interestExpense?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ExtraordinaryItems                = y.extraordinaryItems?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               SellingGeneralAdministrative      = y.sellingGeneralAdministrative?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NonRecurring                      = y.nonRecurring?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherItems                        = y.otherItems?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               IncomeTaxExpense                  = y.incomeTaxExpense?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetIncomeFromContinuingOps        = y.netIncomeFromContinuingOps?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetIncomeApplicableToCommonShares = y.netIncomeApplicableToCommonShares?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               DiscontinuedOperations            = y.discontinuedOperations?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               EffectOfAccountingCharges         = y.effectOfAccountingCharges?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               IncomeBeforeTax                   = y.incomeBeforeTax?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               Ebit                              = y.ebit?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OperatingIncome                   = y.operatingIncome?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetIncome                         = y.netIncome?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               GrossProfit                       = y.grossProfit?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                           };

            db.IncomeStatementQuarterlies
                .Merge()
                .Using(qNewData)
                .OnTargetKey()
                .InsertWhenNotMatched(y => new IncomeStatementQuarterly
                {
                    SecurityID                        = y.SecurityID,
                    Year                              = y.Year,
                    Quarter                           = y.Quarter,
                    EndDate                           = y.EndDate,
                    TotalRevenue                      = y.TotalRevenue,
                    CostOfRevenue                     = y.CostOfRevenue,
                    ResearchDevelopment               = y.ResearchDevelopment,
                    TotalOperatingExpenses            = y.TotalOperatingExpenses,
                    TotalOtherIncomeExpenseNet        = y.TotalOtherIncomeExpenseNet,
                    OtherOperatingExpenses            = y.OtherOperatingExpenses,
                    MinorityInterest                  = y.MinorityInterest,
                    InterestExpense                   = y.InterestExpense,
                    ExtraordinaryItems                = y.ExtraordinaryItems,
                    SellingGeneralAdministrative      = y.SellingGeneralAdministrative,
                    NonRecurring                      = y.NonRecurring,
                    OtherItems                        = y.OtherItems,
                    IncomeTaxExpense                  = y.IncomeTaxExpense,
                    NetIncomeFromContinuingOps        = y.NetIncomeFromContinuingOps,
                    NetIncomeApplicableToCommonShares = y.NetIncomeApplicableToCommonShares,
                    DiscontinuedOperations            = y.DiscontinuedOperations,
                    EffectOfAccountingCharges         = y.EffectOfAccountingCharges,
                    IncomeBeforeTax                   = y.IncomeBeforeTax,
                    Ebit                              = y.Ebit,
                    OperatingIncome                   = y.OperatingIncome,
                    NetIncome                         = y.NetIncome,
                    GrossProfit                       = y.GrossProfit,
                })
                .UpdateWhenMatched((target, y) => new IncomeStatementQuarterly()
                {
                    SecurityID                        = y.SecurityID,
                    Year                              = y.Year,
                    Quarter                           = y.Quarter,
                    EndDate                           = y.EndDate,
                    TotalRevenue                      = y.TotalRevenue,
                    CostOfRevenue                     = y.CostOfRevenue,
                    ResearchDevelopment               = y.ResearchDevelopment,
                    TotalOperatingExpenses            = y.TotalOperatingExpenses,
                    TotalOtherIncomeExpenseNet        = y.TotalOtherIncomeExpenseNet,
                    OtherOperatingExpenses            = y.OtherOperatingExpenses,
                    MinorityInterest                  = y.MinorityInterest,
                    InterestExpense                   = y.InterestExpense,
                    ExtraordinaryItems                = y.ExtraordinaryItems,
                    SellingGeneralAdministrative      = y.SellingGeneralAdministrative,
                    NonRecurring                      = y.NonRecurring,
                    OtherItems                        = y.OtherItems,
                    IncomeTaxExpense                  = y.IncomeTaxExpense,
                    NetIncomeFromContinuingOps        = y.NetIncomeFromContinuingOps,
                    NetIncomeApplicableToCommonShares = y.NetIncomeApplicableToCommonShares,
                    DiscontinuedOperations            = y.DiscontinuedOperations,
                    EffectOfAccountingCharges         = y.EffectOfAccountingCharges,
                    IncomeBeforeTax                   = y.IncomeBeforeTax,
                    Ebit                              = y.Ebit,
                    OperatingIncome                   = y.OperatingIncome,
                    NetIncome                         = y.NetIncome,
                    GrossProfit                       = y.GrossProfit,
                })
                .Merge();
        }
    }
}
