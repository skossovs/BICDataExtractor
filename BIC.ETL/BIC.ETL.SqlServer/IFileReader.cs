using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    public interface IFileReader<T>
    {
        IEnumerable<T> Read();
        bool Next();
    }
}
