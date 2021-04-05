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

            headers = null; data = null;
            cq = cq.Find(@"div[data-ractid=""51""]");
            _logger.Debug(cq.Html());
            //// Find Headers
            //var cqHeaders = cq.Find(@"td[class^=""table-top""]");
            //var headersList = new List<string>() { "IgnoreIt" };
            //// Display headers
            //foreach (var h in cqHeaders)
            //{
            //    // remove image if found
            //    var headerCaption = h.InnerHTML;
            //    var imageEndIndex = headerCaption.IndexOf('>');
            //    if (imageEndIndex != -1)
            //        headerCaption = headerCaption.Remove(0, imageEndIndex + 1);
            //    headersList.Add(headerCaption);
            //}
            //headers = headersList.Skip(1).ToArray(); // Remove first empty field from the headers
            //var headers1 = headersList.ToArray();
            //_logger.Debug("*********************************** Extracted table ***********************************");
            //_logger.Debug(headers.Aggregate("", (s1, s2) => s1 + "|" + s2));

            //// Find Rows & Cells
            //var cellsInTheRow = headers1.Count();
            //string[] currentRow = new string[cellsInTheRow];
            //var lstCells = new List<string[]>();
            //int iCell = 0;

            //var cqCellsInLine = cq.Find(@"tr[class$=""-row-cp""]"); // ends with -row-cp
            //foreach (var cell in cqCellsInLine.Contents())
            //{
            //    // Extract Cells
            //    var cqCells = cqHelper.InitiateWithContent(cell.Render());
            //    var cellValue = cqCells.Contents().Text();
            //    currentRow[iCell] = cellValue;
            //    iCell++;
            //    if (iCell == cellsInTheRow)
            //    {
            //        lstCells.Add(currentRow.Skip(1).ToArray());
            //        _logger.Debug(currentRow.Aggregate("", (s1, s2) => s1 + "|" + s2 + "|"));
            //        currentRow = new string[cellsInTheRow];
            //        iCell = 0;
            //    }
            //}

            //data = lstCells.AsEnumerable();
            return true;
        }











    }
}
