using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.DataObjects
{
    public class SecurityRecord
    {
        public int    SecurityID       { get; set; }
        public string Ticker           { get; set; }
        public int    SectorID         { get; set; }
        public string Sector           { get; set; }
        public int    IndustryID       { get; set; }
        public string Industry         { get; set; }
    }
}
