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
            // 2. Merge Industry information
            // 3. Merge Security information
        }

        public IEnumerable<OverviewData> Read()
        {
            var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
            return JsonConvert.DeserializeObject<IEnumerable<OverviewData>>(jsonContent);
        }
    }
}
