using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class PageMetric
    {
        public int RecordsPerPage {
            get
            {
                return 20; // fixed for Finviz
            }
        }
        public int? NumberOfPages { get; set; }
    }
}
