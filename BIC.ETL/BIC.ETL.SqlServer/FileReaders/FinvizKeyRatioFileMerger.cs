using System;
using System.Collections.Generic;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using Newtonsoft.Json;
using BIC.Utils;
using System.Linq;
using LinqToDB;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Configuration;
using BIC.ETL.SqlServer.DataLayer;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class FinvizKeyRatioFileMerger : IFileMerger<FinancialData>
    {
        private string                    _fileName;
        private DateTime                  _datestamp;
        public FinvizKeyRatioFileMerger(string fileName, DateTime datestamp)
        {
            _fileName = fileName;
            _datestamp = datestamp;
        }


        public void Merge(IEnumerable<FinancialData> newData)
        {
            // get existing key ratio by quarter
            var yq = _datestamp.DateToYearQuarter();
            var year = yq.Item1;
            var quarter = yq.Item2;

            var db = DataConnectionFactory.CreateInstance();

            var qNewData = from f in newData
                            join s in db.Securities.Select(s1 => new { s1.SecurityID, s1.Ticker }) on f.Ticker equals s.Ticker
                            select new DataLayer.KeyRatio
                            {
                                SecurityID = s.SecurityID,
                                Change = f.Change,
                                CurrentRatio = f.CurrentRatio,
                                DebtToEquity = f.DebtToEquity,
                                Dividend = f.Dividend,
                                Earnings = f.Earnings,
                                GrossMargin = f.GrossMargin,
                                LongTermDebtToEquity = f.LongTermDebtToEquity,
                                MarketCap = f.MarketCap,
                                OperationMargin = f.OperationMargin,
                                Price = f.Price,
                                ProfitMargin = f.ProfitMargin,
                                Quarter = quarter,
                                Year = year,
                                QuickRatio = f.QuickRatio,
                                ROA = f.ROA,
                                ROE = f.ROE,
                                ROI = f.ROI,
                                Volume = f.Volume
                            };

            db.KeyRatios
                .Merge()
                .Using(qNewData)
                .OnTargetKey()
                .InsertWhenNotMatched(src => new KeyRatio()
                {
                    SecurityID = src.SecurityID,
                    Year = src.Year,
                    Quarter = src.Quarter,
                    CurrentRatio = src.CurrentRatio,
                    DebtToEquity = src.DebtToEquity,
                    Dividend = src.Dividend,
                    Earnings = src.Earnings,
                    GrossMargin = src.GrossMargin,
                    LongTermDebtToEquity = src.LongTermDebtToEquity,
                    MarketCap = src.MarketCap,
                    OperationMargin = src.OperationMargin,
                    Price = src.Price,
                    ProfitMargin = src.ProfitMargin,
                    QuickRatio = src.QuickRatio,
                    ROA = src.ROA,
                    ROE = src.ROE,
                    ROI = src.ROI,
                    Volume = src.Volume
                })
                .UpdateWhenMatched((target, src) => new KeyRatio()
                {
                    SecurityID = src.SecurityID,
                    Year = src.Year,
                    Quarter = src.Quarter,
                    CurrentRatio = src.CurrentRatio,
                    DebtToEquity = src.DebtToEquity,
                    Dividend = src.Dividend,
                    Earnings = src.Earnings,
                    GrossMargin = src.GrossMargin,
                    LongTermDebtToEquity = src.LongTermDebtToEquity,
                    MarketCap = src.MarketCap,
                    OperationMargin = src.OperationMargin,
                    Price = src.Price,
                    ProfitMargin = src.ProfitMargin,
                    QuickRatio = src.QuickRatio,
                    ROA = src.ROA,
                    ROE = src.ROE,
                    ROI = src.ROI,
                    Volume = src.Volume
                })
                .Merge();

            // Update existing attributes
            // Implement quarterly logic


        }

        public IEnumerable<FinancialData> Read()
        {
             var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
             return JsonConvert.DeserializeObject<IEnumerable<FinancialData>>(jsonContent);
        }
    }
}
