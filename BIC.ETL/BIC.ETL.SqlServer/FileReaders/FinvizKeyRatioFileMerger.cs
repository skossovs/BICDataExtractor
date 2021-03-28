using System;
using System.Collections.Generic;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using Newtonsoft.Json;
using BIC.Utils;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class FinvizKeyRatioFileMerger : IFileMerger<FinancialData>
    {
        private string   _fileName;
        private DateTime _datestamp;
        private string   _connectionString;

        public FinvizKeyRatioFileMerger(string fileName, DateTime datestamp)
        {
            _fileName = fileName;
            _datestamp = datestamp;
            _connectionString = Settings.GetInstance().SQLConnectionString;
        }


        public void Merge(IEnumerable<FinancialData> newData)
        {
            var context = new DataLayer.BICContext(_connectionString);

            // get existing key ratio by quarter
            var yq      = _datestamp.DateToYearQuarter();
            var year    = yq.Item1;
            var quarter = yq.Item2;

            //var exitingKR = context.KeyRatio.Select(s => s).Where(s1 => s1.Year == year && s1.Quarter == quarter);

            var exitingKR =  context.KeyRatio
                .AsEnumerable()
                .Select(s => new DataLayer.KeyRatioData
                {
                        SecurityID           = s.SecurityID,
                        Change               = s.Change,
                        CurrentRatio         = s.CurrentRatio,
                        DebtToEquity         = s.DebtToEquity,
                        Dividend             = s.Dividend,
                        Earnings             = s.Earnings,
                        GrossMargin          = s.GrossMargin,
                        LongTermDebtToEquity = s.LongTermDebtToEquity,
                        MarketCap            = s.MarketCap,
                        OperationMargin      = s.OperationMargin,
                        Price                = s.Price,
                        ProfitMargin         = s.ProfitMargin,
                        Quarter              = s.Quarter,
                        Year                 = s.Year,
                        QuickRatio           = s.QuickRatio,
                        ROA                  = s.ROA,
                        ROE                  = s.ROE,
                        ROI                  = s.ROI,
                        Volume               = s.Volume
                    })
                .Where(s1 => s1.Year == year && s1.Quarter == quarter);

            var qNewData = from f in newData
                           join s in context.Security.Select(s1 => new { s1.SecurityID, s1.Ticker }) on f.Ticker equals s.Ticker
                           select new DataLayer.KeyRatioData
                           {
                               SecurityID           = s.SecurityID,
                               Change               = f.Change,
                               CurrentRatio         = f.CurrentRatio,
                               DebtToEquity         = f.DebtToEquity,
                               Dividend             = f.Dividend,
                               Earnings             = f.Earnings,
                               GrossMargin          = f.GrossMargin,
                               LongTermDebtToEquity = f.LongTermDebtToEquity,
                               MarketCap            = f.MarketCap,
                               OperationMargin      = f.OperationMargin,
                               Price                = f.Price,
                               ProfitMargin         = f.ProfitMargin,
                               Quarter              = quarter,
                               Year                 = year,
                               QuickRatio           = f.QuickRatio,
                               ROA                  = f.ROA,
                               ROE                  = f.ROE,
                               ROI                  = f.ROI,
                               Volume               = f.Volume
                           };

            var toBeInserted = qNewData.Select(s => s).Except(exitingKR);

            // Insert new data in Key Ratio table
            context.KeyRatio.InsertAllOnSubmit(toBeInserted);
            context.SubmitChanges();
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
