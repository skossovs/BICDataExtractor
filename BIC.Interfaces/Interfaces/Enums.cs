using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    public enum DataSources    { Finviz = 0, Yahoo = 1, MoneyConverter = 2 };
    public enum DataSources    { Finviz = 0, Yahoo = 1  };
    public enum ProcessResult  { SUCCESS = 0, FATAL = -1, STOPPED = 1, FORCIBLY_CLOSED=-1073741510 };

    public enum EProcessCommand { Stop = 1, Kill = 2 };
    public enum EProcessStatus  { Finished = 0, Running = 1,  Stopped = 2, Killed = 3 };
    public enum EProcessType    { Scrapper, ETL };
}
