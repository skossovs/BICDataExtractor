using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class FinvizPager<T> : IPager<T> where T : FinvizParameters
    {
        private static ILog _logger = LogServiceProvider.Logger;

        public bool DefineMetrics(T requestParameters, out int recordsPerPage, out int maxPage)
        {
            var requestData = Conversions.FromFinvizParametersToHttpRequestData(requestParameters);
            var url         = requestData.GenerateAddressRequest();

            var htmlContent = RequestHelper.GetData(url);
            var cqHelper    = new CQHelper();
            var cq          = cqHelper.InitiateWithContent(htmlContent);

            var pgmScrapper = new FragmentScrappers.PageMetricScrapper();

            var rendered = cq.Render();
            string pageBody = pgmScrapper.FindRawContent(rendered);

            if (pageBody.IsNullOrEmtpy())
            {
                _logger.Error("Page metric raw content is wrong, whole page content is listed below: ");
                _logger.Error(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                _logger.Error(htmlContent);
                _logger.Error(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                recordsPerPage = 0; maxPage = 0;
                return false;
            }

            var pageInfo = pgmScrapper.CallParsers(pageBody);

            recordsPerPage = pageInfo.First().RecordsPerPage;

            if (!pageInfo.First().NumberOfPages.HasValue)
            {
                recordsPerPage = -1;
                maxPage        = -1;
                _logger.Debug("page fragment: {0}", pageBody);
                _logger.Debug("rendered content: {0}", rendered);
                return false;
            }

            maxPage = pageInfo.First().NumberOfPages.Value;
            return true;
        }
    }
}
