using BIC.ETL.SqlServer.DataLayer;
using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.Utils;
using BIC.Utils.Logger;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class YahooBalanceSheetMerger : FileReader<BalanceSheetData>, IFileMerger<BalanceSheetData>
    {
        private ILog _logger = LogServiceProvider.Logger;

        public YahooBalanceSheetMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<BalanceSheetData> newData)
        {
            var db = DataConnectionFactory.CreateInstance();
            var qNewData = from y in newData
                           join s in db.Securities.Select(s1 => new { s1.SecurityID, s1.Ticker }) on y.Ticker equals s.Ticker
                           select new BalanceSheetQuarterly
                           {
                               SecurityID = s.SecurityID,
                               Year       = Convert.ToDateTime(y.endDate.fmt).DateToYearQuarter().Item1,
                               Quarter    = -1,
                               EndDate    = Convert.ToDateTime(y.endDate.fmt),
                               Cash                    = y.cash?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               IntangibleAssets        = y.intangibleAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherCurrentAssets      = y.otherCurrentAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalCurrentAssets      = y.totalCurrentAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               GoodWill                = y.goodWill?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               RetainedEarnings        = y.retainedEarnings?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               PropertyPlantEquipment  = y.propertyPlantEquipment?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               LongTermInvestments     = y.longTermInvestments?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ShortTermInvestments    = y.shortTermInvestments?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetReceivables          = y.netReceivables?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               Inventory               = y.inventory?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               AccountsPayable         = y.accountsPayable?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherAssets             = y.otherAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalAssets             = y.totalAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherCurrentLiab        = y.otherCurrentLiab?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalCurrentLiabilities = y.totalCurrentLiabilities?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               ShortLongTermDebt       = y.shortLongTermDebt?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherLiab               = y.otherLiab?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               LongTermDebt            = y.longTermDebt?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalLiab               = y.totalLiab?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               NetTangibleAssets       = y.netTangibleAssets?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TotalStockholderEquity  = y.totalStockholderEquity?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               CommonStock             = y.commonStock?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               OtherStockholderEquity  = y.otherStockholderEquity?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                               TreasuryStock           = y.treasuryStock?.raw.StringToDecimal(e => _logger.Error(e.Message)),
                           };

            db.BalanceSheetQuarterlies
                .Merge()
                .Using(qNewData)
                .OnTargetKey()
                .InsertWhenNotMatched(y => new BalanceSheetQuarterly
                {
                    SecurityID              = y.SecurityID,
                    Year                    = y.Year,
                    Quarter                 = y.Quarter,
                    EndDate                 = y.EndDate,
                    Cash                    = y.Cash,
                    IntangibleAssets        = y.IntangibleAssets,
                    OtherCurrentAssets      = y.OtherCurrentAssets,
                    TotalCurrentAssets      = y.TotalCurrentAssets,
                    GoodWill                = y.GoodWill,
                    RetainedEarnings        = y.RetainedEarnings,
                    PropertyPlantEquipment  = y.PropertyPlantEquipment,
                    LongTermInvestments     = y.LongTermInvestments,
                    ShortTermInvestments    = y.ShortTermInvestments,
                    NetReceivables          = y.NetReceivables,
                    Inventory               = y.Inventory,
                    AccountsPayable         = y.AccountsPayable,
                    OtherAssets             = y.OtherAssets,
                    TotalAssets             = y.TotalAssets,
                    OtherCurrentLiab        = y.OtherCurrentLiab,
                    TotalCurrentLiabilities = y.TotalCurrentLiabilities,
                    ShortLongTermDebt       = y.ShortLongTermDebt,
                    OtherLiab               = y.OtherLiab,
                    LongTermDebt            = y.LongTermDebt,
                    TotalLiab               = y.TotalLiab,
                    NetTangibleAssets       = y.NetTangibleAssets,
                    TotalStockholderEquity  = y.TotalStockholderEquity,
                    CommonStock             = y.CommonStock,
                    OtherStockholderEquity  = y.OtherStockholderEquity,
                    TreasuryStock           = y.TreasuryStock,
                })
                .UpdateWhenMatched((target, y) => new BalanceSheetQuarterly()
                {
                    SecurityID              = y.SecurityID,
                    Year                    = y.Year,
                    Quarter                 = y.Quarter,
                    EndDate                 = y.EndDate,
                    Cash                    = y.Cash,
                    IntangibleAssets        = y.IntangibleAssets,
                    OtherCurrentAssets      = y.OtherCurrentAssets,
                    TotalCurrentAssets      = y.TotalCurrentAssets,
                    GoodWill                = y.GoodWill,
                    RetainedEarnings        = y.RetainedEarnings,
                    PropertyPlantEquipment  = y.PropertyPlantEquipment,
                    LongTermInvestments     = y.LongTermInvestments,
                    ShortTermInvestments    = y.ShortTermInvestments,
                    NetReceivables          = y.NetReceivables,
                    Inventory               = y.Inventory,
                    AccountsPayable         = y.AccountsPayable,
                    OtherAssets             = y.OtherAssets,
                    TotalAssets             = y.TotalAssets,
                    OtherCurrentLiab        = y.OtherCurrentLiab,
                    TotalCurrentLiabilities = y.TotalCurrentLiabilities,
                    ShortLongTermDebt       = y.ShortLongTermDebt,
                    OtherLiab               = y.OtherLiab,
                    LongTermDebt            = y.LongTermDebt,
                    TotalLiab               = y.TotalLiab,
                    NetTangibleAssets       = y.NetTangibleAssets,
                    TotalStockholderEquity  = y.TotalStockholderEquity,
                    CommonStock             = y.CommonStock,
                    OtherStockholderEquity  = y.OtherStockholderEquity,
                    TreasuryStock           = y.TreasuryStock,
                })
                .Merge();
        }
    }
}
