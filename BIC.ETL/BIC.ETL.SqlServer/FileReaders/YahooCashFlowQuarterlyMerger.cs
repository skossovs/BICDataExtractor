using System;
using BIC.Scrappers.YahooScrapper.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class YahooCashFlowQuarterlyMerger : FileReader<CashFlowDataQuarterly>, IFileMerger<CashFlowDataQuarterly>
    {
        public YahooCashFlowQuarterlyMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<CashFlowDataQuarterly> newData)
        {
            throw new NotImplementedException();
        }
    }
}
