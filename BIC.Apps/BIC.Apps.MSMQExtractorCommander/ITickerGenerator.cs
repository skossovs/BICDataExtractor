using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    interface ITickerGenerator
    {
        IEnumerable<string> GetTickers();
    }
}
