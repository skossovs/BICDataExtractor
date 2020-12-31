using BIC.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class FinvizPager : IPager
    {
        public readonly string UrlRoot = Settings.GetInstance().UrlRoot;

        public bool DefineMetrics(out int recordsPerPage, out int maxPage)
        {

            throw new NotImplementedException();
        }

        public IEnumerable<string> GenerateRequestAdresses()
        {
            throw new NotImplementedException();
        }
    }
}
