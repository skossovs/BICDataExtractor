using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Monads
{
    public static class Cyclic
    {
        public static void StoppableWhile(Func<bool> isTimeToStop, Func<bool> StepCheckIfFinished)
        {
            bool isFinished = false;
            while(!isTimeToStop() && isFinished)
            {
                isFinished = StepCheckIfFinished();
            }
        }
    }
}
