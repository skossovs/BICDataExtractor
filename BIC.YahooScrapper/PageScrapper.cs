using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.Utils.Logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                    Ticker     = requestParameters.Ticker
                ,   ReportType = requestParameters.ReportType
            };
            var generatedAddress = r.GenerateAddressRequest();

            // 2.1. Get string table from current page
            string[] header;
            IEnumerable<string[]> cells;
            bool result = false;
            Exception ex = null;

            if (requestParameters.IsQuarterly)
            {
                if (r.ReportType == "financials")
                {
                    result = ProcessData<IncomeStatementDataQuarterly>(requestParameters, generatedAddress, out ex);
                }
                else if (r.ReportType == "balance-sheet")
                {
                    result = ProcessData<BalanceSheetDataQuarterly>(requestParameters, generatedAddress, out ex);
                }
                else if (r.ReportType == "cash-flow")
                {
                    result = ProcessData<CashFlowDataQuarterly>(requestParameters, generatedAddress, out ex);
                }

            }
            else // TODO: not fiinished Yearly part
            {
                result = GetStringTableFromCurrentPageYearly(generatedAddress, out header, out cells);
                result = false;
            }
            if (!result)
            {
                _logger.Error(ex.StackTrace);
                _logger.Error(ex.Message);
            }

            return result;
        }

        private bool ProcessData<QT>(YahooParameters requestParameters, string generatedAddress, out Exception ex) where QT : QuarterData
        {
            IEnumerable<QT> allPageData;
            bool result = GetStringTableFromCurrentPageQuarterly(generatedAddress, requestParameters, out allPageData);

            if (result == false)
                _logger.Warning("Failed to extract table data");

            // 3. Save List of types into file system as json file
            var fileName = FileHelper.ComposeFileName(typeof(QT), true, "json");
            var fullPath = System.IO.Path.Combine(Settings.GetInstance().OutputDirectory, fileName);
            _logger.Info("Saving data into json file: " + fullPath);

            return FileHelper.SaveAsJSON(allPageData, fullPath, out ex);
        }

        private bool GetStringTableFromCurrentPageQuarterly<QT>(string generatedAddress, YahooParameters requestParameters, out IEnumerable<QT> data)
            where QT:QuarterData
        {
            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var currentPagehtmlContent = retriever.GetData(generatedAddress);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            data = null;
            // process json inside response
            // find script
            var scripts = cq.Find("script");
            _logger.Debug("*********************************** Extract Quarterly Json ***********************************");
            foreach (var s in scripts.Contents())
            {
                var content = s.Render();
                var startIndex = content.IndexOf("root.App.main = ");
                if (startIndex == -1)
                    continue;
                startIndex += 16;

                var endIndex = content.IndexOf("(this));", startIndex);

                var jsonString = content.Substring(startIndex, endIndex - startIndex - 3); // TODO: magic numbers

                jsonString = jsonString.Replace("&quot;", @"""");
                //_logger.Debug(jsonString);
                //var jsonObject = JsonConvert.DeserializeObject(jsonString);

                var jsonObject = JObject.Parse(jsonString);

                string jsonPath = "";
                if (typeof(QT).Name == "IncomeStatementDataQuarterly")
                    jsonPath = "context.dispatcher.stores.QuoteSummaryStore.incomeStatementHistoryQuarterly";
                else if(typeof(QT).Name == "BalanceSheetDataQuarterly")
                    jsonPath = "context.dispatcher.stores.QuoteSummaryStore.balanceSheetHistoryQuarterly";
                else if (typeof(QT).Name == "CashFlowDataQuarterly")
                    jsonPath = "context.dispatcher.stores.QuoteSummaryStore.cashflowStatementHistoryQuarterly";
                else
                {
                    _logger.Error("Unsupported type: {0}", typeof(QT).Name);
                    throw new Exception("Unsupported type: " + typeof(QT).Name);
                }

                var jIncomeToken = jsonObject.SelectToken(jsonPath);
                var lst = new List<QT>();

                foreach (var jt in jIncomeToken.First.Values())
                {
                    var v = jt.ToObject<QT>();
                    v.Ticker = requestParameters.Ticker;
                    lst.Add(v);
                }
                data = lst.AsEnumerable<QT>();

                //_logger.Debug("JTOKEN:");
                //_logger.Debug(JsonConvert.SerializeObject(jIncomeToken));
            }
            return true;
        }
        private bool GetStringTableFromCurrentPageYearly(string generatedAddress, out string[] headers, out IEnumerable<string[]> data)
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
