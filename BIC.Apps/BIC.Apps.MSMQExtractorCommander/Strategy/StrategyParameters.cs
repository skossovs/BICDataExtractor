using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class StrategyParameters
    {
        public string Sector { get; set; }
        public string[] AllSectors { get; set; }
        public string TickerAt { get; set; }
    }
}
