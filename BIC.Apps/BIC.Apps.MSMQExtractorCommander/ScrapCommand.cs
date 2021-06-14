using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    public abstract class ScrapCommand
    {
        public IBridgeComponents _iBridgeComponents;
        public abstract void Scrap<T>() where T : class, new();
    }
}
