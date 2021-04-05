using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper
{
    public class PageScrapper<T> where T : class, new()
    {
        private ILog _logger = LogServiceProvider.Logger;
        public bool Scrap(YahooParameters requestParameters)
        {
            var r = new HttpRequestData()
            {
                    Ticker = requestParameters.Ticker
                ,   ReportType = requestParameters.ReportType
            };
            var generatedAddress = r.GenerateAddressRequest();

            // 2.1. Get string table from current page
            string[] header;
            IEnumerable<string[]> cells;
            if (!GetStringTableFromCurrentPage(generatedAddress, out header, out cells))
            {
                _logger.Warning("Failed to extract table data");
                return false;
            }

            return true;
        }

        private bool GetStringTableFromCurrentPage(string generatedAddress, out string[] headers, out IEnumerable<string[]> data)
        {
            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var currentPagehtmlContent = retriever.GetData(generatedAddress);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            if (cq.Elements.Count() == 0)
            {
                _logger.Warning("Request returns no elements");
                headers = null; data = null;
                return false;
            }

            headers = null;
            var cqHeaders = cq.Find(@"div[class=""D(tbr) C($primaryColor)""]");
            _logger.Debug("Headers text fragment: {0}", cqHeaders.Html());
            //// Find Headers
                cqHeaders   = cqHeaders.Find(@"span[data-reactid]");
            var headersList = new List<string>();
            //// Display headers
            foreach (var h in cqHeaders)
            {
                var headerCaption = h.InnerHTML;
                headersList.Add(headerCaption);
            }
            headers = headersList.ToArray();
            _logger.Debug("*********************************** Extracted Header ***********************************");
            _logger.Debug(headers.Aggregate("", (s1, s2) => s1 + "|" + s2));

            //// Find Rows & Cells
            var cqData = cq.Find(@"div[class=""D(tbrg)""]");
                cqData = cqData.Find(@"div[data-test=""fin-row""]");
            _logger.Debug("Data text fragment: {0}", cqData.Html());
            var cellsInTheRow = headers.Count();
            string[] currentRow = new string[cellsInTheRow];
            var lstCells = new List<string[]>();
            int iCell = 0;

            //TODO: clean up failed filters
            //var cqCellsInLine = cqData.Find(@"div[data-test=""fin-col""],span[data-reactid]");   span class="Va(m)"
            //var cqCellsInLine = cqData.Find(@"div[class^=""D(tbc) Ta(start)""],div[class^=""Ta(c) Py(6px) Bxz(bb)""]");
            var cqCellsInLine = cqData.Find(@"span[class=""Va(m)""],div[class^=""Ta(c) Py(6px) Bxz(bb)""]");
            _logger.Debug("*********************************** Extracted Data ***********************************");
            foreach (var cell in cqCellsInLine.Contents())
            {
                // Extract Cells
                var cqCell        = cqHelper.InitiateWithContent(cell.Render());
                var cellValue     = cqCell.Contents().Text();
                currentRow[iCell] = cellValue;
                iCell++;
                if (iCell == cellsInTheRow)
                {
                    lstCells.Add(currentRow.ToArray());
                    _logger.Debug(currentRow.Aggregate("", (s1, s2) => s1 + "|" + s2 + "|"));
                    currentRow = new string[cellsInTheRow];
                    iCell = 0;
                }
            }

            data = lstCells.AsEnumerable();
            return true;
        }











    }
}
