using BIC.Foundation.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer.FileManagers
{
    public class SecurityMasterFileReader : IFileReader<SecurityRecord>
    {
        public bool Next()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SecurityRecord> Read()
        {
            throw new NotImplementedException();
        }
    }
}
