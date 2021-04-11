﻿using BIC.Foundation.Interfaces;
using BIC.Scrappers.FinvizScrapper.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper.FragmentScrappers
{
    public class PageMetricScrapper : IScrapper<PageMetric>
    {
        public IEnumerable<PageMetric> CallParsers(string parsingFragment)
        {
            var parser = new PageParser();
            PageMetric pageMetric = parser.ParseObject(parsingFragment);
            return (new PageMetric[] { pageMetric }).AsEnumerable();
        }

        public string FindRawContent(string pageContent)
        {
            var cqHelper = new Utils.CQHelper();
            var cq = cqHelper.InitiateWithContent(pageContent);
            return cq.Find(@"select[id=""pageSelect""]").Contents().Text();
        }

        public IEnumerable<string> SeparateIntoRecordsContent(string rawContent)
        {
            throw new NotImplementedException();
        }
    }
}
