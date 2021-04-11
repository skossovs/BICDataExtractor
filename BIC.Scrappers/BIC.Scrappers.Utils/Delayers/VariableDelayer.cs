using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils.Delayers
{
    // minDelay..maxDelay  pick random number from interval
    public class VariableDelayer : Delayer
    {
        protected int minDelay;
        protected int maxDelay;
        public VariableDelayer()
        {
            var arr = Settings.GetInstance().IntervalTimeDelayInSeconds.Split(new string[] { ".." }, StringSplitOptions.RemoveEmptyEntries);
            minDelay = Convert.ToInt32(arr[0]) * 1000;
            maxDelay = Convert.ToInt32(arr[1]) * 1000;
        }

        public override void Wait()
        {
            var rnd   = new Random(DateTime.Now.Millisecond);
            int delay = rnd.Next(minDelay, maxDelay);
            Thread.Sleep(delay);
        }
    }
}
