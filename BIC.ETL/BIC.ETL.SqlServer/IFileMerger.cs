using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    public interface IFileMerger<T>
    {
        IEnumerable<T> Read();
        void Merge(IEnumerable<T> newData);
    }
}
