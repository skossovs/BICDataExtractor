using BIC.ETL.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServerTest
{
    [TestClass]
    public class TestArchivarius
    {
        [TestMethod]
        public void TryToArchive()
        {
            string path = "E:\\_DATA"; // TODO: hardcoded path

            // read first
            var a = new FileArchivarius();

            a.Archive(System.IO.Path.Combine(path, "BIC.YahooScrapper_CashFlowDataQuarterly_4-9-2021 12-34-16 AM.json"));
            a.Archive(System.IO.Path.Combine(path, "BIC.YahooScrapper_BalanceSheetDataQuarterly_4-9-2021 12-33-27 AM.json"));
            a.Archive(System.IO.Path.Combine(path, "BIC.YahooScrapper_IncomeStatementDataQuarterly_4-9-2021 12-32-51 AM.json"));
            a.Archive(System.IO.Path.Combine(path, "BIC.YahooScrapper_CashFlowDataQuarterly_4-9-2021 12-31-54 AM.json"));

            Thread.Sleep(2000);
        }
    }
}
