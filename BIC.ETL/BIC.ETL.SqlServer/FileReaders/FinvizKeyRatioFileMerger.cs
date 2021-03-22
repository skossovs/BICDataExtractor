using System;
using System.Collections.Generic;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class FinvizKeyRatioFileMerger : IFileMerger<FinancialData>
    {
        private string _fileName;
        private DateTime _datestamp;
        private string _connectionString;

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
            //var quarter = _datestamp

            // Insert new data in Key Ratio table
            // Update existing attributes
            // Implement quarterly logic
            throw new NotImplementedException();
        }

        public IEnumerable<FinancialData> Read()
        {
             var jsonContent = (new System.IO.StreamReader(_fileName)).ReadToEnd();
             return JsonConvert.DeserializeObject<IEnumerable<FinancialData>>(jsonContent);
        }
    }
}
