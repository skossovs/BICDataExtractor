﻿using BIC.Foundation.Interfaces;
using BIC.MoneyConverterScrapper.DataObjects;
using BIC.Scrappers.Utils;
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

            string[] headers = new string[] { "picture", "Currency", "Rate" };
            IEnumerable<string[]> data = new List<string[]>();

            _logger.Debug("** Major-Currency-Table **");
            result = ProcessData(@"table[id=""major-currency-table""]",  headers, ref data);
            if (!result)
                _logger.Error("Major Currency table failed to load");

            _logger.Debug("** Minor-Currency-Table **");
            result = ProcessData(@"table[id=""minor-currency-table""]",  headers, ref data);
            if (!result)
                _logger.Error("Minor Currency table failed to load");

            _logger.Debug("** Exotic-Currency-Table **");
            result = ProcessData(@"table[id=""exotic-currency-table""]", headers, ref data);
            if (!result)
                _logger.Error("Exotic Currency table failed to load");

            return result;
        }

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

            var content = cq.Find(tableSection).Find("tbody");
            foreach (var tr in content.Contents())
            {
                var trContent = tr.Render();
                var cqCells = cqHelper.InitiateWithContent(trContent);

                foreach (var cell in cqCells.Find("td").Contents())
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
            }

            data.Concat(lstCells.AsEnumerable());
            return true;
        }
    }
}
