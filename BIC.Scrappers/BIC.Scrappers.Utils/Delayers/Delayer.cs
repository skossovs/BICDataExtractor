using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils.Delayers
{
    public class Delayer
    {
        protected readonly int FixedTimeDelay = Settings.GetInstance().TimeDelayInSeconds * 1000;
        public virtual void Wait()
        {
            Thread.Sleep(FixedTimeDelay);
        }
    }
}
