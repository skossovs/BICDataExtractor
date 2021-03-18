using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.DataLayer
{
    public class BICContext : DataContext
    {
        public BICContext(string connString) : base(connString)  {  }
        public Table<IndustryData> Industry;
        public Table<SectorData>   Sector;
        public Table<SecurityData> Security;
    }
}
