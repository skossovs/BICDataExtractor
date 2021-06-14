using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    class FxRatesBridgeComponents : IBridgeComponents
    {
        public void Scrap<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
