using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.DataLayer
{
    [Table(Name = "TimeDimmension")]
    public class TimeDimmensionData
    {
        [Column(IsPrimaryKey = true)]
        public int TimeID;
        public int Year;
        public int Quarter;
    }
}
