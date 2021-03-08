using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public enum EFilterView
    {
        Descriptive = 1,
        Fundamental = 2,
        Technical   = 3,
        All         = 4,
    };

    public enum EView
    {
        Overview    = 111,
        Valuation   = 121,
        Financial   = 161,
        Ownership   = 131,
        Performance = 141,
        Technical   = 171,
        Basic       = 311,
        TA          = 351,
        Snapshot    = 341,
    };

    public class Filters
    {
        public string ExchangeFilter { get; set; }
        public string IndexFilter    { get; set; }
        public string SectorFilter   { get; set; }
        public string IndustryFilter { get; set; }
        public string CountrFilter   { get; set; }
    }

    public class FinvizParameters
    {
        public EFilterView? FilterView     { get; set; }
        public EView?       View           { get; set; }
        public Filters      Filters        { get; set; }
        public int?         PageAsR        { get; set; }
    }
}
