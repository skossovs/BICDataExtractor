using BIC.Scrappers.FinvizScrapper.DataObjects;
using BIC.Scrappers.Utils.TableScrapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ScrapperTests
{
    [TestClass]
    public class TEST_TableReaderBasedOnFinviz
    {
        private string[] _headers = { "No.", "Ticker", "Company", "Sector", "Industry", "Country", "Market Cap", "P/E", "Price", "Change", "Volume" };

        private IEnumerable<string[]> _cells = new List<string[]>() { new string[11] { "1", "AA", "Alcoa Corp", "Basic Material", "Aluminum", "USA", "5540000000", "-", "28.59", "-7.27%", "10349081" } };

        [TestMethod]
        public void TestOverviewTab()
        {
            var referenceObject = new OverviewData()
            {
                No          = 1,
                Country     = "USA",
                FullName    = "Alcoa Corp",
                Industry    = "Aluminum",
                MarketCap   = 5540000000,
                PE          = null,
                Price       = (decimal) 28.59,
                PriceChange = (decimal) -0.0727,
                Sector      = "Basic Material",
                Ticker      = "AA",
                Volume      = 10349081
            };

            var tr = new TableReader<OverviewData>();
            bool headersGenerated = tr.MapHeader(_headers);
            Assert.IsTrue(headersGenerated);

            var data = tr.GenerateDataSet(_cells);
            Assert.AreEqual(1, data.Count());
            Assert.AreEqual(referenceObject.Industry, data.First().Industry);
            Assert.AreEqual(referenceObject.Country, data.First().Country);
            Assert.AreEqual(referenceObject.FullName, data.First().FullName);
            Assert.AreEqual(referenceObject.MarketCap, data.First().MarketCap);
            Assert.AreEqual(referenceObject.PriceChange, data.First().PriceChange);
            Assert.AreEqual(referenceObject.PE, data.First().PE);
        }
    }
}
