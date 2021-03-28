using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.DataLayer
{
    [Table(Name = "KeyRatio")]
    public class KeyRatioData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
                 public int      KeyRatioID;
        [Column] public int      SecurityID;
        [Column] public int      Year;
        [Column] public int      Quarter;
        [Column] public decimal? MarketCap;
        [Column] public decimal? Dividend;
        [Column] public decimal? ROA;
        [Column] public decimal? ROE;
        [Column] public decimal? ROI;
        [Column] public decimal? CurrentRatio;
        [Column] public decimal? QuickRatio;
        [Column] public decimal? LongTermDebtToEquity;
        [Column] public decimal? DebtToEquity;
        [Column] public decimal? GrossMargin;
        [Column] public decimal? OperationMargin;
        [Column] public decimal? ProfitMargin;
        [Column] public decimal? Earnings;
        [Column] public decimal? Price;
        [Column] public decimal? Change;
        [Column] public decimal? Volume;
    }
}
