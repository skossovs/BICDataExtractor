using BIC.Scrappers.YahooScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.YahooScrapper.Chain
{
    public class Context
    {
        public YahooParameters Parameters       { get; set; }
        public List<string>    JsonContentLines { get; set; }

        public Context()
        {
            JsonContentLines = new List<string>();
        }
    }
}
