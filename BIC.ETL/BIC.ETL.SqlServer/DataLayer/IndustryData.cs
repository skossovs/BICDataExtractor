using System.Data.Linq.Mapping;

namespace BIC.ETL.SqlServer.DataLayer
{
    [Table(Name = "Industry")]
    public class IndustryData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int IndustryID;
        [Column] public int SectorID;
        [Column] public string Industry;
    }
}
