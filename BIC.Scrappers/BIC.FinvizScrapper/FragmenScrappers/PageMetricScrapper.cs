using BIC.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper.FragmenScrappers
{
    public class PageMetricScrapper : IScrapper<PageMetric>
    {
        public IEnumerable<PageMetric> CallParsers()
        {
            throw new NotImplementedException();
        }

        public string FindRawContent(string pageContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SeparateIntoRecordsContent(string rawContent)
        {
            throw new NotImplementedException();
        }
    }
}
