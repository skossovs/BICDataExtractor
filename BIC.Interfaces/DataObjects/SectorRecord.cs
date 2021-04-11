using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.DataObjects
{
    public class SectorRecord
    {
        public int SectorID  { get; set; }
        public string Sector { get; set; }

        public List<IndustryRecord> Industries { get; set; }
    }
}
