using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper
{
    public class YahooParameters
    {
        public string Ticker { get; set; }
        public string ReportType { get; set; }
        public bool   IsQuarterly { get; set; }
    }
}
