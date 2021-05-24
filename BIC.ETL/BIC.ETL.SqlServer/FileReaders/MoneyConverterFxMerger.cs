using BIC.ETL.SqlServer.DataLayer;
using BIC.MoneyConverterScrapper.DataObjects;
using BIC.Utils;
using BIC.Utils.Logger;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileReaders
{
    public class MoneyConverterFxMerger : FileReader<FxUsdData>, IFileMerger<FxUsdData>
    {
        private ILog _logger = LogServiceProvider.Logger;

        public MoneyConverterFxMerger(string fileName)
        {
            _fileName = fileName;
        }
        public void Merge(IEnumerable<FxUsdData> newData)
        {
            var db = DataConnectionFactory.CreateInstance();
            Tuple<int, int> yearQuarter = DateTime.Now.DateToYearQuarter();

            var qNewData = from f in newData
                           select new FxUsdRate() {
                               Currency = f.Currency,
                               Rate     = f.Rate,
                               Year     = yearQuarter.Item1,
                               Quarter  = yearQuarter.Item2 };
            db.FxUsdRates
                .Merge()
                .Using(qNewData)
                .OnTargetKey()
                .InsertWhenNotMatched(f => new FxUsdRate
                {
                    Currency = f.Currency,
                    Country  = "-",
                    Rate     = f.Rate,
                    Year     = f.Year,
                    Quarter  = f.Quarter
                })
                .UpdateWhenMatched((target, f) => new FxUsdRate
                {
                    Rate     = f.Rate,
                })
                .Merge();
        }
    }
}
