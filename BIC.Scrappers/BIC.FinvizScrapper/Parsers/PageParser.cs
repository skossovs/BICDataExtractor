using BIC.Foundation.Interfaces;
using BIC.Utils;
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
        public PageMetric ParseObject(string fragment)
        {
            // Page 1/1
//            var result = new PageMetric() { NumberOfPages = fragment.Replace("Page ", "").With(s => s.MapClassToNullable(s1 => s1)) };
            throw new NotImplementedException();
        }

        public PageMetric ParseObject(string fragment, ref PageMetric obj)
        {
            throw new NotImplementedException();
        }
    }
}
