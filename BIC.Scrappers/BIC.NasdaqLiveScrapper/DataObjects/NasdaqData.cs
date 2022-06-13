using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.NasdaqLiveScrapper.DataObjects
{
    public class NasdaqData
    {
        public string nlsTime { get; set; }
        public string nlsPrice { get; set; }
        public string nlsShareVolume { get; set; }
    }
}
