using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class FinvizPager<T> : IPager<T> where T : FinvizParameters
    {
        public bool DefineMetrics(T requestParameters, out int recordsPerPage, out int maxPage)
        {
            var requestData = Conversions.FromFinvizParametersToHttpRequestData(requestParameters);
            var url = requestData.GenerateAddressRequest();
            var cqHelper = new CQHelper();
            var cq = cqHelper.GetData(url);

            var pgmScrapper = new FragmenScrappers.PageMetricScrapper();
            string pageBody = pgmScrapper.FindRawContent(cq.Render());
            var pageInfo = pgmScrapper.CallParsers(pageBody);

            recordsPerPage = pageInfo.First().RecordsPerPage;

            if (!pageInfo.First().NumberOfPages.HasValue)
            {
                recordsPerPage = -1;
                maxPage        = -1;
                return false;
            }

            maxPage = pageInfo.First().NumberOfPages.Value;
            return true;
        }

        public IEnumerable<string> GenerateRequestAdresses()
        {
            throw new NotImplementedException();
        }
    }
}
