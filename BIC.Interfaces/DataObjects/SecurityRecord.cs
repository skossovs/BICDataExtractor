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
        public int    SectorRecordID   { get; set; }
        public int    IndustryRecordID { get; set; }
    }
}
