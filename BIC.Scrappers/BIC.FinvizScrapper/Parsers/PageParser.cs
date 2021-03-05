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
        private char[] _page_separator = ("Page ").ToCharArray();
        public PageMetric ParseObject(string fragment)
        {
            // Page 1/1
            return new PageMetric()
            {
                NumberOfPages = fragment
                    .Split(_page_separator, StringSplitOptions.RemoveEmptyEntries).Skip(1).First()
                    .Split('/').Skip(1).First()
                    .StringToInt(s => _logger.Error($"Can't parse page fragment {s}", s)),
            };
        }

        public PageMetric ParseObject(string fragment, ref PageMetric obj)
        {
            // TODO: Design incorrect ??
            throw new NotImplementedException();
        }
    }
}
