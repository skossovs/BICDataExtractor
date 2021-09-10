using BIC.Scrappers.FinvizScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Logger;

namespace BIC.Apps.MSMQExtractorCommander
{
    public class StoppableStatusable : IStoppableStatusable
    {
        // TODO: analyse the log and search for signals
        public bool IsStopped => throw new NotImplementedException();

        public event StopperEventHandler Stopper;

        public ILog OverrideLogger(ILog originalLogger)
        {
            return new UIStatusLogger(originalLogger);
        }
    }
}
