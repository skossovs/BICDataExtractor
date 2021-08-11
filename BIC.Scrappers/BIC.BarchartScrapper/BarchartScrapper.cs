using BIC.BarchartScrapper.DataObjects;
using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.BarchartScrapper
{
    public class BarchartScrapper<T> where T : BarchartData
    {
        private ILog _logger = LogServiceProvider.Logger;
        private string _url = Settings.GetInstance().UrlRoot;

        public bool Scrap()
        {
            // 1.1. Get string table from current page
            bool result = false;

            string[] header = new string[] { "Symbol", "Name", "Std Dev", "Last", "Change", "%Chg", "High", "Low", "Volume", "Time", "Links" };
            IEnumerable<string[]> data = new List<string[]>();

            _logger.Debug("** Barchart-Bullish-Table **");
            result = ProcessData(@"div[class=""bc-table-scrollable""]", header, ref data);
            if (!result)
            {
                _logger.Error("Barchart bullish table failed to load");
                return result;
            }


            return result;
        }

        // TODO: Copy-paste from FxScrapper
        private bool ProcessData(string tableSection, string[] headers, ref IEnumerable<string[]> data)
        {
            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var currentPagehtmlContent = retriever.GetData(_url);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            var cellsInTheRow = headers.Count();
            string[] currentRow = new string[cellsInTheRow];
            var lstCells = new List<string[]>();
            int iCell = 0;

            var content = cq.Find(tableSection);

            _logger.Debug(content.Render());
            foreach (var tr in content.Contents())
            {
                var trContent = tr.Render();
                var cqCells = cqHelper.InitiateWithContent(trContent).Find("body");

            }

            return true;
        }
    }
}
