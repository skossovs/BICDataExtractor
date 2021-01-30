using BIC.Foundation.Interfaces;
using BIC.Utils;
using BIC.Utils.Logger;
using BIC.Utils.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper.Parsers
{
    public class PageParser : IParser<PageMetric>
    {
        private ILog _logger = LogServiceProvider.Logger;
        public PageMetric ParseObject(string fragment)
        {
            // Page 1/1
            return new PageMetric()
            {
                NumberOfPages = fragment
                    .Replace("Page ", "")
                    .StringToInt(s => _logger.Error($"Can't parse page fragment {s}", s)),
            };
        }

        public PageMetric ParseObject(string fragment, ref PageMetric obj)
        {
            throw new NotImplementedException();
        }
    }
}
