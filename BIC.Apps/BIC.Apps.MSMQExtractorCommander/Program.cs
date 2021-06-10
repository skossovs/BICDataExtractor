using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    class Program
    {
        static int Main(string[] args)
        {
            for(int i = 0; i < args.Count(); i++)
            {
                Console.WriteLine(args[i]);
            }

            System.Threading.Thread.Sleep(10000);

            return (int) BIC.Foundation.Interfaces.ProcessResult.SUCCESS;
        }
    }
}
