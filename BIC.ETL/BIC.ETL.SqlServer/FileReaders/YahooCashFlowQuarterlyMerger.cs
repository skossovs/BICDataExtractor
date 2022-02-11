﻿using System;
using BIC.Scrappers.YahooScrapper.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.ETL.SqlServer.DataLayer;
using BIC.Utils;
using LinqToDB;
using BIC.Utils.Logger;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class YahooCashFlowQuarterlyMerger : FileReader<CashFlowDataQuarterly>, IFileMerger<CashFlowDataQuarterly>
    {
        private ILog _logger = LogServiceProvider.Logger;
        public YahooCashFlowQuarterlyMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<CashFlowDataQuarterly> newData)
        {
            var db = DataConnectionFactory.CreateInstance();
            var qNewData = from y in newData
                           join s in db.Securities.Select(s1 => new { s1.SecurityID, s1.Ticker }) on y.Ticker equals s.Ticker
                           select new CashFlowQuarterly()
                           {
                               SecurityID = s.SecurityID,
                               Year       = Convert.ToDateTime(y.endDate.fmt).DateToYearQuarter().Item1,
                               Quarter    = Convert.ToDateTime(y.endDate.fmt).DateToYearQuarter().Item2,
                               EndDate    = Convert.ToDateTime(y.endDate.fmt),
                               Investments                           = y.investments?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetBorrowings                         = y.netBorrowings?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetIncome                             = y.netIncome?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               IssuanceOfStock                       = y.issuanceOfStock?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               RepurchaseOfStock                     = y.repurchaseOfStock?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               EffectOfExchangeRate                  = y.effectOfExchangeRate?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               Depreciation                          = y.depreciation?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               DividendsPaid                         = y.dividendsPaid?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ChangeInCash                          = y.changeInCash?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ChangeToLiabilities                   = y.changeToLiabilities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ChangeToOperatingActivities           = y.changeToOperatingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ChangeToInventory                     = y.changeToInventory?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ChangeToAccountReceivables            = y.changeToAccountReceivables?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherCashflowsFromInvestingActivities = y.otherCashflowsFromInvestingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherCashflowsFromFinancingActivities = y.otherCashflowsFromInvestingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalCashflowsFromInvestingActivities = y.otherCashflowsFromInvestingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalCashFromFinancingActivities      = y.totalCashflowsFromInvestingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalCashFromOperatingActivities      = y.totalCashFromOperatingActivities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               CapitalExpenditures                   = y.capitalExpenditures?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                           };

            // Correct quarter datestamp due late filling.
            qNewData = LateFillingDatesCorrectionPerQuarter(qNewData);

            db.CashFlowQuarterlies
                .Merge()
                .Using(qNewData)
                .OnTargetKey()
                .InsertWhenNotMatched(y => new CashFlowQuarterly()
                {
                    SecurityID                            = y.SecurityID,
                    Year                                  = y.Year,
                    Quarter                               = y.Quarter,
                    EndDate                               = y.EndDate,
                    Investments                           = y.Investments,
                    NetBorrowings                         = y.NetBorrowings,
                    NetIncome                             = y.NetIncome,
                    IssuanceOfStock                       = y.IssuanceOfStock,
                    RepurchaseOfStock                     = y.RepurchaseOfStock,
                    EffectOfExchangeRate                  = y.EffectOfExchangeRate,
                    Depreciation                          = y.Depreciation,
                    DividendsPaid                         = y.DividendsPaid,
                    ChangeInCash                          = y.ChangeInCash,
                    ChangeToLiabilities                   = y.ChangeToLiabilities,
                    ChangeToOperatingActivities           = y.ChangeToOperatingActivities,
                    ChangeToInventory                     = y.ChangeToInventory,
                    ChangeToAccountReceivables            = y.ChangeToAccountReceivables,
                    OtherCashflowsFromInvestingActivities = y.OtherCashflowsFromInvestingActivities,
                    OtherCashflowsFromFinancingActivities = y.OtherCashflowsFromInvestingActivities,
                    TotalCashflowsFromInvestingActivities = y.OtherCashflowsFromInvestingActivities,
                    TotalCashFromFinancingActivities      = y.TotalCashflowsFromInvestingActivities,
                    TotalCashFromOperatingActivities      = y.TotalCashFromOperatingActivities,
                    CapitalExpenditures                   = y.CapitalExpenditures,
                })
                .UpdateWhenMatched((target, y) => new CashFlowQuarterly()
                {
                    SecurityID                            = y.SecurityID,
                    Year                                  = y.Year,
                    Quarter                               = y.Quarter,
                    EndDate                               = y.EndDate,
                    Investments                           = y.Investments,
                    NetBorrowings                         = y.NetBorrowings,
                    NetIncome                             = y.NetIncome,
                    IssuanceOfStock                       = y.IssuanceOfStock,
                    RepurchaseOfStock                     = y.RepurchaseOfStock,
                    EffectOfExchangeRate                  = y.EffectOfExchangeRate,
                    Depreciation                          = y.Depreciation,
                    DividendsPaid                         = y.DividendsPaid,
                    ChangeInCash                          = y.ChangeInCash,
                    ChangeToLiabilities                   = y.ChangeToLiabilities,
                    ChangeToOperatingActivities           = y.ChangeToOperatingActivities,
                    ChangeToInventory                     = y.ChangeToInventory,
                    ChangeToAccountReceivables            = y.ChangeToAccountReceivables,
                    OtherCashflowsFromInvestingActivities = y.OtherCashflowsFromInvestingActivities,
                    OtherCashflowsFromFinancingActivities = y.OtherCashflowsFromInvestingActivities,
                    TotalCashflowsFromInvestingActivities = y.OtherCashflowsFromInvestingActivities,
                    TotalCashFromFinancingActivities      = y.TotalCashflowsFromInvestingActivities,
                    TotalCashFromOperatingActivities      = y.TotalCashFromOperatingActivities,
                    CapitalExpenditures                   = y.CapitalExpenditures,
                })
                .Merge();
        }

        // TODO: unify with T instead of balance, income, cashflow
        private IEnumerable<CashFlowQuarterly> LateFillingDatesCorrectionPerQuarter(IEnumerable<CashFlowQuarterly> listOfFour)
        {
            // check if we have duplicate quarters:
            int result = 1;
            foreach (var l in listOfFour)
            {
                result *= l.Quarter;
            }

            if (result != 24 && listOfFour.Count() == 4)
            {  // correct incorrect quarters
                var arr = listOfFour.ToArray(); // TODO: not buitiful
                arr[0].Quarter = 4;
                arr[1].Quarter = 3;
                arr[2].Quarter = 2;
                arr[3].Quarter = 1;
                return arr;
            }
            else
                return listOfFour;
        }
    }
}
