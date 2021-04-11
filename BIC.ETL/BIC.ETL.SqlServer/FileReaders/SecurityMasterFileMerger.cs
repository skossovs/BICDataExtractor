using BIC.Foundation.DataObjects;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using LinqToDB.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
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
            // TODO: make it common
            var connectionString = Settings.GetInstance().SQLConnectionString;
            // create options builder
            var builder = new LinqToDbConnectionOptionsBuilder();
            // configure connection string
            var options = builder.UseSqlServer(connectionString).Build();

            // TODO: use LINQ2DB Merge instead
            using (var db = new DataLayer.BICDB(options))
            {
                // 1. Merge Sector information
                var existingSectors = db.Sectors.Select(s => s.SectorColumn);
                var toBeInserted = newData
                    .GroupBy(o => o.Sector)
                    .Select(kv => new DataLayer.Sector { SectorColumn = kv.Key })
                    .Where(s => !existingSectors.Contains(s.SectorColumn));

                db.Insert(toBeInserted);

                // 2. Merge Industry information
                var existingIndustries = db.Industries.Select(s => new { s.SectorID, s.IndustryColumn });
                var qIndustry = from data in newData
                                join sector in db.Sectors.Select(t => new { t.SectorID, t.SectorColumn }) on data.Sector equals sector.SectorColumn
                                select new { data.Industry, sector.SectorID };
                var toBeInsertedIndustries = qIndustry
                    .GroupBy(x => new { x.SectorID, x.Industry }, (key, group) => new { key.SectorID, key.Industry })
                    .Select(kv => new DataLayer.Industry { SectorID = kv.SectorID, IndustryColumn = kv.Industry })
                    .Where(s => !existingIndustries.Contains(new { s.SectorID, s.IndustryColumn }));

                db.Insert(toBeInsertedIndustries);
                // 3. Merge Security information
                var existingSecurities = db.Securities.Select(s => new { s.SectorID, s.IndustryID, s.Ticker });
                var qSecurity = from data        in newData
                                join sector      in db.Sectors.Select   (t => new { t.SectorID, t.SectorColumn })
                                on   data.Sector equals sector.SectorColumn
                                join industry    in db.Industries.Select(t1 =>     new { t1.IndustryID, t1.IndustryColumn, t1.SectorID })
                                on   new { data.Industry, sector.SectorID } equals new { Industry = industry.IndustryColumn, industry.SectorID }
                                select new { data.Ticker, data.FullName, data.Country, industry.SectorID, industry.IndustryID };
                var toBeInsertedSecurities = qSecurity
                    .Select(s => new DataLayer.Security { SectorID = s.SectorID, IndustryID = s.IndustryID, Ticker = s.Ticker, Company = s.FullName, Country = s.Country })
                    .Where(s1 => !existingSecurities.Contains(new { s1.SectorID, s1.IndustryID, s1.Ticker }));

                db.Insert(toBeInsertedSecurities);
            }
        }

        public IEnumerable<OverviewData> Read()
        {
            var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<OverviewData>>(jsonContent);
        }
    }
}
