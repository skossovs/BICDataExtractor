using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    // TODO: rename, doesn't really a quarter but Yahoo json format
    public class QuarterNumber
    {
        public string raw     { get; set; }
        public string fmt     { get; set; }
        public string longFmt { get; set; }
    }
}
