using BIC.Foundation.DataObjects;
using BIC.Utils;
using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.DataLayer
{
    public static class SecurityReader
    {
        public class CompareCondition
        {
            public int SecurityID;
            public int? Year;
            public int Quarter;
        }
        public static int Year { get; set; }
        public static int Quarter { get; set; }

        static SecurityReader()
        {
            var yq  = DateTime.Now.DateToYearQuarter(-1);
            Year    = yq.Item1;
            Quarter = yq.Item2;
        }

        public static IEnumerable<SectorRecord> GetSectors()
        {
            List<SectorRecord> lst;
            using (var db = DataConnectionFactory.CreateNewInstance())
            {
                var q = from s in db.Sectors
                    select new SectorRecord()
                    {
                        SectorID = s.SectorID,
                        Sector = s.SectorColumn
                    };
                lst = q.AsEnumerable().ToList();
            }
            return lst.AsEnumerable();
        }
        public static IEnumerable<SecurityRecord> GetSecurities()
        {
            List<SecurityRecord> lst;
            using (var db = DataConnectionFactory.CreateNewInstance())
            {
                var q = from s  in db.Securities
                    join i  in db.Industries         on s.IndustryID equals i.IndustryID
                    join sc in db.Sectors            on s.SectorID   equals sc.SectorID
                    join chk in db.LoadConsistencies on new CompareCondition() { SecurityID = s.SecurityID, Year = SecurityReader.Year, Quarter = SecurityReader.Quarter } equals new CompareCondition() { SecurityID = chk.SecurityID, Year = chk.Year, Quarter = chk.Quarter }
                    where s.Type == "SEC" && chk.BalanceSheetQuarterly == 0 // checking BalanceSheetQuarterly is enough so far
                    select new SecurityRecord() {
                        SecurityID = s.SecurityID, Ticker   = s.Ticker
                      , SectorID   = s.SectorID,   Sector   = sc.SectorColumn
                      , IndustryID = i.IndustryID, Industry = i.IndustryColumn
                      , IsBalanceSheetQuarterly    = (chk.BalanceSheetQuarterly == 1)
                      , IsCashFlowQuarterly        = (chk.CashFlowQuarterly == 1)
                      , IsIncomeStatementQuarterly = (chk.IncomeStatementQuarterly == 1)
                    };
                lst = q.AsEnumerable().ToList();
            }
            return lst.AsEnumerable();
        }

        public static SecurityRecord GetSecurityByTicker(string ticker)
        {
            SecurityRecord securityRecord;
            using (var db = DataConnectionFactory.CreateNewInstance())
            {
                var q = from s   in db.Securities
                        join i in db.Industries on s.IndustryID equals i.IndustryID
                        join sc in db.Sectors on s.SectorID equals sc.SectorID
                        join chk in db.LoadConsistencies on new CompareCondition() { SecurityID = s.SecurityID, Year = SecurityReader.Year, Quarter = SecurityReader.Quarter } equals new CompareCondition() { SecurityID = chk.SecurityID, Year = chk.Year, Quarter = chk.Quarter }
                        where s.Ticker == ticker && s.Type == "SEC" && chk.BalanceSheetQuarterly == 0
                        select new SecurityRecord()
                        {
                             SecurityID = s.SecurityID, Ticker = s.Ticker
                          ,  SectorID = s.SectorID,     Sector = sc.SectorColumn
                          ,  IndustryID = i.IndustryID, Industry = i.IndustryColumn
                          ,  IsBalanceSheetQuarterly = (chk.BalanceSheetQuarterly == 1)
                          ,  IsCashFlowQuarterly = (chk.CashFlowQuarterly == 1)
                          ,  IsIncomeStatementQuarterly = (chk.IncomeStatementQuarterly == 1)
                        };
                securityRecord = q.AsEnumerable().FirstOrDefault();
            }

            return securityRecord;
        }

        public static IEnumerable<SecurityRecord> GetSecurities(string sector)
        {
            List<SecurityRecord> lst;
            using (var db = DataConnectionFactory.CreateNewInstance())
            {
                var q = from s in db.Securities
                        join i in db.Industries on s.IndustryID equals i.IndustryID
                        join sc in db.Sectors on s.SectorID equals sc.SectorID
                        join chk in db.LoadConsistencies on new CompareCondition() { SecurityID = s.SecurityID, Year = SecurityReader.Year, Quarter = SecurityReader.Quarter } equals new CompareCondition() { SecurityID = chk.SecurityID, Year = chk.Year, Quarter = chk.Quarter }
                        where sc.SectorColumn == sector && s.Type == "SEC" && chk.BalanceSheetQuarterly == 0 // checking BalanceSheetQuarterly is enough so far
                        select new SecurityRecord() {
                            SecurityID = s.SecurityID, Ticker = s.Ticker
                          , SectorID = s.SectorID, Sector = sc.SectorColumn
                          , IndustryID = i.IndustryID, Industry = i.IndustryColumn
                          , IsBalanceSheetQuarterly = (chk.BalanceSheetQuarterly == 1)
                          , IsCashFlowQuarterly = (chk.CashFlowQuarterly == 1)
                          , IsIncomeStatementQuarterly = (chk.IncomeStatementQuarterly == 1)
                        };
                lst = q.AsEnumerable().ToList();
            }
            return lst.AsEnumerable();
        }
    }
}
