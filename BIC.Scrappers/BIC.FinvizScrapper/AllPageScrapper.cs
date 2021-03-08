using BIC.Foundation.Interfaces;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using BIC.Scrappers.Utils;
using BIC.Scrappers.Utils.TableScrapping;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class AllPageScrapper<T> where T : class, new()
    {
        private ILog _logger = LogServiceProvider.Logger;
        public void Scrap(FinvizParameters requestParameters)
        {
            var pageMetrics = GetFirstPageMetrics(requestParameters);
            for (int i = 1; i < pageMetrics.NumberOfPages; i++)
            {
                requestParameters.PageAsR = ConvertPageToR(i);
                var r = Conversions.FromFinvizParametersToHttpRequestData(requestParameters);
                var generatedAddress = r.GenerateAddressRequest();
                // 2.1. Get string table from current page
                string[] header;
                IEnumerable<string[]> cells;
                if(!GetStringTableFromCurrentPage(generatedAddress, out header, out cells))
                {
                    _logger.Warning("Failed to extract table data");
                    return;
                }
                // 2.2. Convert string page into List of types
                var tr = new TableReader<T>();
                if (!tr.MapHeader(header))
                {
                    _logger.Warning("Failed to map header to class object");
                    return;
                }

                var data = tr.GenerateDataSet(cells);
            }
            // 3. Save List of types into file system as json file

        }

        private PageMetric GetFirstPageMetrics(FinvizParameters requestParameters)
        {
            var pager = new FinvizPager<FinvizParameters>();
            int recordsPerPage = 0;
            int maxPage = 0;
            bool result = pager.DefineMetrics(requestParameters, out recordsPerPage, out maxPage);

            return new PageMetric() { NumberOfPages = maxPage };
        }
        private bool GetStringTableFromCurrentPage(string generatedAddress, out string[] headers, out IEnumerable<string[]> data)
        {
            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData(generatedAddress);
            if (cq.Elements.Count() == 0)
            {
                _logger.Warning("Request returns no elements");
                headers = null; data = null;
                return false;
            }

            cq = cq.Find(@"table[bgcolor=""#d3d3d3""]");

            // Find Headers
            var cqHeaders = cq.Find(@"td[class^=""table-top""]");
            var headersList = new List<string>() { "IgnoreIt" };
            // Display headers
            foreach (var h in cqHeaders)
            {
                // remove image if found
                var headerCaption = h.InnerHTML;
                var imageEndIndex = headerCaption.IndexOf('>');
                if (imageEndIndex != -1)
                    headerCaption = headerCaption.Remove(0, imageEndIndex + 1);
                headersList.Add(headerCaption);
            }
            headers = headersList.Skip(1).ToArray(); // Remove first empty field from the headers
            var headers1 = headersList.ToArray();
            _logger.Debug("*********************************** Extracted table ***********************************");
            _logger.Debug(headers.Aggregate("", (s1, s2) => s1 + "|" + s2));

            // Find Rows & Cells
            var cellsInTheRow = headers1.Count();
            string[] currentRow = new string[cellsInTheRow];
            var lstCells = new List<string[]>();
            int iCell = 0;

            var cqCellsInLine = cq.Find(@"tr[class$=""-row-cp""]"); // ends with -row-cp
            foreach (var cell in cqCellsInLine.Contents())
            {
                // Extract Cells
                var cqCells = cqHelper.InitiateWithContent(cell.Render());
                var cellValue = cqCells.Contents().Text();
                currentRow[iCell] = cellValue;
                iCell++;
                if (iCell == cellsInTheRow)
                {
                    lstCells.Add(currentRow.Skip(1).ToArray());
                    _logger.Debug(currentRow.Aggregate("", (s1, s2) => s1 + "|" + s2 + "|"));
                    currentRow = new string[cellsInTheRow];
                    iCell = 0;
                }
            }

            data = lstCells.AsEnumerable();
            return true;
        }
        private int? ConvertPageToR(int page)
        {
            if (page == 1)
                return null;

            return (page - 1) * 20 + 1;
        }
    }
}
