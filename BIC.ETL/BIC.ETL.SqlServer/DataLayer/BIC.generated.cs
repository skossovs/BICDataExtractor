//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1591

using System;
using System.Linq;

using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Mapping;

namespace BIC.ETL.SqlServer.DataLayer
{
    /// <summary>
    /// Database       : BIC
    /// Data Source    : STAN-PC
    /// Server Version : 11.00.3128
    /// </summary>
    public partial class BICDB : LinqToDB.Data.DataConnection
    {
        public ITable<Industry> Industries { get { return this.GetTable<Industry>(); } }
        public ITable<KeyRatio> KeyRatios { get { return this.GetTable<KeyRatio>(); } }
        public ITable<Sector> Sectors { get { return this.GetTable<Sector>(); } }
        public ITable<Security> Securities { get { return this.GetTable<Security>(); } }
        public ITable<TimeDimmension> TimeDimmensions { get { return this.GetTable<TimeDimmension>(); } }

        public BICDB()
        {
            InitDataContext();
            InitMappingSchema();
        }

        public BICDB(string configuration)
            : base(configuration)
        {
            InitDataContext();
            InitMappingSchema();
        }

        public BICDB(LinqToDbConnectionOptions options)
            : base(options)
        {
            InitDataContext();
            InitMappingSchema();
        }

        partial void InitDataContext();
        partial void InitMappingSchema();
    }

    [Table(Schema = "dbo", Name = "Industry")]
    public partial class Industry
    {
        [Column(), PrimaryKey, Identity] public int IndustryID { get; set; } // int
        [Column(), NotNull] public int SectorID { get; set; } // int
        [Column("Industry"), NotNull] public string IndustryColumn { get; set; } // nvarchar(200)
    }

    [Table(Schema = "dbo", Name = "KeyRatio")]
    public partial class KeyRatio
    {
        [PrimaryKey(1), NotNull] public int SecurityID { get; set; } // int
        [PrimaryKey(2), NotNull] public int Year { get; set; } // int
        [PrimaryKey(3), NotNull] public int Quarter { get; set; } // int
        [Column, Nullable] public decimal? MarketCap { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? Dividend { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? ROA { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? ROE { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? ROI { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? CurrentRatio { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? QuickRatio { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? LongTermDebtToEquity { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? DebtToEquity { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? GrossMargin { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? OperationMargin { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? ProfitMargin { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? Earnings { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? Price { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? Change { get; set; } // numeric(18, 6)
        [Column, Nullable] public decimal? Volume { get; set; } // numeric(18, 6)
    }

    [Table(Schema = "dbo", Name = "Sector")]
    public partial class Sector
    {
        [Column(), PrimaryKey, Identity] public int SectorID { get; set; } // int
        [Column("Sector"), Nullable] public string SectorColumn { get; set; } // nvarchar(200)
    }

    [Table(Schema = "dbo", Name = "Security")]
    public partial class Security
    {
        [PrimaryKey, Identity] public int SecurityID { get; set; } // int
        [Column, NotNull] public int SectorID { get; set; } // int
        [Column, NotNull] public int IndustryID { get; set; } // int
        [Column, NotNull] public string Ticker { get; set; } // nvarchar(6)
        [Column, NotNull] public string Company { get; set; } // nvarchar(200)
        [Column, NotNull] public string Country { get; set; } // nvarchar(50)
    }

    [Table(Schema = "dbo", Name = "TimeDimmension")]
    public partial class TimeDimmension
    {
        [PrimaryKey, NotNull] public int TimeID { get; set; } // int
        [Column, NotNull] public int Year { get; set; } // int
        [Column, NotNull] public int Quarter { get; set; } // int
    }

    public static partial class TableExtensions
    {
        public static Industry Find(this ITable<Industry> table, int IndustryID)
        {
            return table.FirstOrDefault(t =>
                t.IndustryID == IndustryID);
        }

        public static KeyRatio Find(this ITable<KeyRatio> table, int SecurityID, int Year, int Quarter)
        {
            return table.FirstOrDefault(t =>
                t.SecurityID == SecurityID &&
                t.Year == Year &&
                t.Quarter == Quarter);
        }

        public static Sector Find(this ITable<Sector> table, int SectorID)
        {
            return table.FirstOrDefault(t =>
                t.SectorID == SectorID);
        }

        public static Security Find(this ITable<Security> table, int SecurityID)
        {
            return table.FirstOrDefault(t =>
                t.SecurityID == SecurityID);
        }

        public static TimeDimmension Find(this ITable<TimeDimmension> table, int TimeID)
        {
            return table.FirstOrDefault(t =>
                t.TimeID == TimeID);
        }
    }
}

#pragma warning restore 1591