using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{

    public interface IStoppableStatusable<TLog>  where TLog : class
    {
        TLog OverrideLogger(TLog originalLogger);
        bool IsStopped { get; }
    }
}
