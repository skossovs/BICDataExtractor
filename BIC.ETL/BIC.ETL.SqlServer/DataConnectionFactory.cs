using BIC.ETL.SqlServer.DataLayer;
using LinqToDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    // TODO: problem with linq2db - standard using directive schema is not working with Merge. Used second time and will have runtime.
    public class DataConnectionFactory
    {
        private static BICDB _db;

        public static BICDB CreateInstance()
        {
            if (_db != null)
                return _db;

            var connectionString = Settings.GetInstance().SQLConnectionString;
            // create options builder
            var builder = new LinqToDbConnectionOptionsBuilder();
            // configure connection string
            var options = builder.UseSqlServer(connectionString).Build();
            _db = new DataLayer.BICDB(options);

            return _db;
        }

        // TODO: to curcumviene same-connection-usage error. only with using sentence
        public static BICDB CreateNewInstance()
        {
            var connectionString = Settings.GetInstance().SQLConnectionString;
            // create options builder
            var builder = new LinqToDbConnectionOptionsBuilder();
            // configure connection string
            var options = builder.UseSqlServer(connectionString).Build();
            var db = new DataLayer.BICDB(options);
            return db;
        }

        #region static destructor
        private static readonly Destructor Finalise = new Destructor();
        private sealed class Destructor
        {
            ~Destructor()
            { // TODO: no good
                try
                {
                    _db.Dispose();
                }
                catch (Exception ex)
                { }
            }
        }
        #endregion
    }
}
