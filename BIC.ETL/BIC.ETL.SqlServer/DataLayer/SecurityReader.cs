using BIC.Foundation.DataObjects;
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
        public static IEnumerable<SectorRecord> GetSectors()
        {
            var db = DataConnectionFactory.CreateInstance();
            var q = from s in db.Sectors
                    select new SectorRecord()
                    {
                        SectorID = s.SectorID,
                        Sector = s.SectorColumn
                    };

            return q.AsEnumerable();
        }
        public static IEnumerable<SecurityRecord> GetSecurities()
        {
            var db = DataConnectionFactory.CreateInstance();
            var q = from s  in db.Securities
                    join i  in db.Industries on s.IndustryID equals i.IndustryID
                    join sc in db.Sectors    on s.SectorID   equals sc.SectorID
                    select new SecurityRecord() {
                        SecurityID = s.SecurityID, Ticker = s.Ticker
                      , SectorID = s.SectorID, Sector = sc.SectorColumn
                      , IndustryID = i.IndustryID, Industry = i.IndustryColumn
                    };

            return q.AsEnumerable();
        }

        public static IEnumerable<SecurityRecord> GetSecurities(string sector)
        {
            var db = DataConnectionFactory.CreateInstance();
            var q = from s  in db.Securities
                    join i  in db.Industries on s.IndustryID equals i.IndustryID
                    join sc in db.Sectors    on s.SectorID   equals sc.SectorID
                    where sc.SectorColumn == sector
                    select new SecurityRecord() {
                        SecurityID = s.SecurityID, Ticker = s.Ticker
                        , SectorID = s.SectorID, Sector = sc.SectorColumn
                        , IndustryID = i.IndustryID, Industry = i.IndustryColumn
                    };

            return q.AsEnumerable();
        }
    }
}
