using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace BIC.ETL.SqlServer.DataLayer
{
    [Table(Name = "Security")]
    public class SecurityData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int SecurityID;
        [Column] public int SectorID;
        [Column] public int IndustryID;
        [Column] public string Ticker;
        [Column] public string Company;
        [Column] public string Country;
    }
}
