using System;
using BIC.Scrappers.YahooScrapper.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class YahooIncomeStatementQuarterlyMerger : FileReader<IncomeStatementDataQuarterly>, IFileMerger<IncomeStatementDataQuarterly>
    {
        public YahooIncomeStatementQuarterlyMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<IncomeStatementDataQuarterly> newData)
        {
            throw new NotImplementedException();
        }
    }
}
