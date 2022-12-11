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

            using (var db = new DataLayer.BICDB(options))
            {
                // 1. Merge Sector information
                var qNewSectors = (from fvz in newData
                                   select new DataLayer.Sector() { SectorColumn = fvz.Sector }).Distinct();

                db.Sectors
                    .Merge()
                    .Using(qNewSectors)
                    .OnTargetKey()
                    .InsertWhenNotMatched(f => new DataLayer.Sector() { SectorColumn = f.SectorColumn })
                    .Merge();

                // 2. Merge Industry information
                var qNewIndustries = (from fvz in newData
                                      join s in db.Sectors.Select(s1 => new { s1.SectorID, s1.SectorColumn }) on fvz.Sector equals s.SectorColumn
                                      select new DataLayer.Industry() { SectorID = s.SectorID, IndustryColumn = fvz.Industry });

                db.Industries
                    .Merge()
                    .Using(qNewIndustries)
                    .OnTargetKey()
                    .InsertWhenNotMatched(f => new DataLayer.Industry() { SectorID = f.SectorID, IndustryColumn = f.IndustryColumn })
                    .Merge();

                // 3. Merge Security information
                var qnewSecurity = from data        in newData
                                join sector      in db.Sectors.Select   (t => new { t.SectorID, t.SectorColumn })
                                on   data.Sector equals sector.SectorColumn
                                join industry    in db.Industries.Select(t1 =>     new { t1.IndustryID, t1.IndustryColumn, t1.SectorID })
                                on   new { data.Industry, sector.SectorID } equals new { Industry = industry.IndustryColumn, industry.SectorID }
                                select new DataLayer.Security() {
                                    Ticker     = data.Ticker,
                                    Company    = data.FullName,
                                    Country    = data.Country,
                                    SectorID   = industry.SectorID,
                                    IndustryID = industry.IndustryID};

                db.Securities
                    .Merge()
                    .Using(qnewSecurity)
                    .OnTargetKey()
                    .InsertWhenNotMatched(s => new DataLayer.Security
                    {
                        Ticker     = s.Ticker,
                        Company    = s.Company,
                        Country    = s.Country,
                        SectorID   = s.SectorID,
                        IndustryID = s.IndustryID,
                        Type       = "SEC"
                    })
                    .Merge(); // one more merge call is needed
            }
        }

        public IEnumerable<OverviewData> Read()
        {
            var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<OverviewData>>(jsonContent);
        }
    }
}
