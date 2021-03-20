using BIC.Foundation.DataObjects;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class SecurityMasterFileMerger : IFileMerger<OverviewData>
    {
        private string   _fileName;
        private DateTime _datestamp;
        private string   _connectionString;

        public SecurityMasterFileMerger(string fileName, DateTime datestamp)
        {
            _fileName  = fileName;
            _datestamp = datestamp;
            _connectionString = Settings.GetInstance().SQLConnectionString;
        }
        public void Merge(IEnumerable<OverviewData> newData)
        {
            var context = new DataLayer.BICContext(_connectionString);
            // 1. Merge Sector information
            var existingSectors = context.Sector.Select(s => s.Sector);
            var toBeInserted = newData
                .GroupBy(o => o.Sector)
                .Select(kv => new DataLayer.SectorData { Sector = kv.Key })
                .Where(s => !existingSectors.Contains(s.Sector));

            context.Sector.InsertAllOnSubmit(toBeInserted);
            context.SubmitChanges();

            // 2. Merge Industry information
            var existingIndustries = context.Industry.Select(s => new { s.SectorID, s.Industry });
            var qIndustry = from data in newData
                            join sector in context.Sector.Select(t => new { t.SectorID, t.Sector }) on data.Sector equals sector.Sector
                            select new { data.Industry, sector.SectorID };
            var toBeInsertedIndustries = qIndustry
                .GroupBy(x => new { x.SectorID, x.Industry }, (key, group) => new { key.SectorID, key.Industry })
                .Select(kv => new DataLayer.IndustryData { SectorID = kv.SectorID, Industry = kv.Industry })
                .Where(s => !existingIndustries.Contains(new { s.SectorID, s.Industry }));

            context.Industry.InsertAllOnSubmit(toBeInsertedIndustries);
            context.SubmitChanges();
            // 3. Merge Security information
            var existingSecurities = context.Security.Select(s => new { s.SectorID, s.IndustryID, s.Ticker });
            var qSecurity = from data     in newData
                            join sector   in context.Sector  .Select(t =>  new { t.SectorID, t.Sector })
                            on   data.Sector equals sector.Sector
                            join industry in context.Industry.Select(t1 => new { t1.IndustryID, t1.Industry, t1.SectorID })
                            on   new { data.Industry, sector.SectorID } equals new { industry.Industry, industry.SectorID }
                            select new { data.Ticker, data.FullName, data.Country, industry.SectorID, industry.IndustryID };
            var toBeInsertedSecurities = qSecurity
                .Select(s => new DataLayer.SecurityData { SectorID = s.SectorID, IndustryID = s.IndustryID, Ticker = s.Ticker, Company = s.FullName, Country = s.Country })
                .Where(s1 => !existingSecurities.Contains(new { s1.SectorID, s1.IndustryID, s1.Ticker }));

            context.Security.InsertAllOnSubmit(toBeInsertedSecurities);
            context.SubmitChanges();

        }

        public IEnumerable<OverviewData> Read()
        {
            var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<OverviewData>>(jsonContent);
        }
    }
}
