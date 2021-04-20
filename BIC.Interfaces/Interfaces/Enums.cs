using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    public enum DataSources    { Finviz = 0, Yahoo = 1  };
    public enum ProcessResult  { SUCCESS = 0, FATAL = -1, STOPPED = 1 };
}
