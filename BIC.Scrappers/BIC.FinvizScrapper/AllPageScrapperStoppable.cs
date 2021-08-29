using System;
using System.Collections.Generic;
using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Scrappers.Utils.TableScrapping;
using BIC.Utils.Logger;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class AllPageScrapperStoppable<T> : AllPageScrapper<T> where T : class, new()
    {

        public AllPageScrapperStoppable()
        {

        }
        public override void Scrap(FinvizParameters requestParameters)
        {
            var pageMetrics = GetFirstPageMetrics(requestParameters);
            var allPageData = new List<T>();

            for (int i = 1; i < pageMetrics.NumberOfPages; i++)
            {
                requestParameters.PageAsR = ConvertPageToR(i);
                var r = Conversions.FromFinvizParametersToHttpRequestData(requestParameters);
                var generatedAddress = r.GenerateAddressRequest();
                // 2.1. Get string table from current page
                string[] header;
                IEnumerable<string[]> cells;
                if (!GetStringTableFromCurrentPage(generatedAddress, out header, out cells))
                {
                    _logger.Warning("Failed to extract table data");
                    return;
                }
                // 2.2. Convert string page into List of types
                var tr = new TableArrayConverter<T>();
                if (!tr.MapHeader(header))
                {
                    _logger.Warning("Failed to map header to class object");
                    return;
                }

                var data = tr.GenerateDataSet(cells);
                allPageData.AddRange(data);
            }

            // 3. Save List of types into file system as json file
            var fileName = FileHelper.ComposeFileName(typeof(T), true, "json");
            var fullPath = System.IO.Path.Combine(Settings.GetInstance().OutputDirectory, fileName);
            _logger.Info("Saving data into json file: " + fullPath);

            Exception ex = null;
            if (!FileHelper.SaveAsJSON(allPageData, fullPath, out ex))
            {
                _logger.Error(ex.StackTrace);
                _logger.Error(ex.Message);
            }
        }
    }
}
