using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.DataLayer
{
    [Table(Name = "Sector")]
    public class SectorData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int SectorID;
        [Column] public string Sector;
    }
}
