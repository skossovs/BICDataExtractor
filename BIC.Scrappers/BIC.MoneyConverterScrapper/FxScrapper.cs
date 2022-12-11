using BIC.Foundation.Interfaces;
using BIC.MoneyConverterScrapper.DataObjects;
using BIC.Scrappers.Utils;
using BIC.Scrappers.Utils.TableScrapping;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.MoneyConverterScrapper
{
    public class FxScrapper<T> where T : FxUsdData
    {
        private ILog   _logger = LogServiceProvider.Logger;
        private string _url    = Settings.GetInstance().UrlRoot;

        public bool Scrap()
        {
            // 1.1. Get string table from current page
            bool result = false;

            string[] header = new string[] { "Picture", "Currency", "Rate" };
            IEnumerable<string[]> data = new List<string[]>();

            _logger.Debug("** Major-Currency-Table **");
            result = ProcessData(@"table[id=""major-currency-table""]",  header, ref data);
            if (!result)
                _logger.Error("Major Currency table failed to load");

            _logger.Debug("** Minor-Currency-Table **");
            result = ProcessData(@"table[id=""minor-currency-table""]",  header, ref data);
            if (!result)
                _logger.Error("Minor Currency table failed to load");

            _logger.Debug("** Exotic-Currency-Table **");
            result = ProcessData(@"table[id=""exotic-currency-table""]", header, ref data);
            if (!result)
                _logger.Error("Exotic Currency table failed to load");

            // Convert string page into List of types
            var tabConverter = new TableArrayConverter<FxUsdData>(); // TODO: syntax is weird
            if (!tabConverter.MapHeader(header))
            {
                _logger.Warning("Failed to map header to class object");
                return false;
            }
            var fxData = tabConverter.GenerateDataSet(data);

            // Save List of types into file system as json file
            var fileName = FileHelper.ComposeFileName(typeof(T), true, "json");
            var fullPath = System.IO.Path.Combine(Settings.GetInstance().OutputDirectory, fileName);
            _logger.Info("Saving data into json file: " + fullPath);

            Exception ex = null;
            if (!FileHelper.SaveAsJSON(fxData, fullPath, out ex))
            {
                _logger.Error(ex.StackTrace);
                _logger.Error(ex.Message);
            }

            return result;
        }

        private bool ProcessData(string tableSection, string[] headers, ref IEnumerable<string[]> data)
        {
            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.MoneyConverter);
            var currentPagehtmlContent = retriever.GetData(_url);
            if(String.IsNullOrEmpty(currentPagehtmlContent))
            {
                _logger.Error("Failed to retrieve FX data - empty page in the output");
                return false;
            }
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            var cellsInTheRow = headers.Count();
            string[] currentRow = new string[cellsInTheRow];
            var lstCells = new List<string[]>();
            int iCell = 0;

            var content = cq.Find(tableSection);
            foreach (var tr in content.Contents())
            {
                var trContent = tr.Render();
                var cqCells = cqHelper.InitiateWithContent(trContent).Find("body");

                foreach (var cell in cqCells.Contents())
                {
                    // Extract Cells
                    var cellValue = "";
                    if (iCell == 0)
                        cellValue = cell.ClassName;
                    else if (iCell == 1)
                        cellValue = cell?.FirstChild?.NodeValue;
                    else if (iCell == 2)
                        cellValue = cell?.NodeValue;

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
            }

            data = data.Concat(lstCells);
            return true;
        }
    }
}
