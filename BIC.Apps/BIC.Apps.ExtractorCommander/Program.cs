using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.ExtractorCommander
{
    class Program
    {
        private static ILog _logger = LogServiceProvider.Logger;

        static void Main(string[] args)
        {
            _logger.Info("Starting..");
            // TODO: implement commands
            _logger.Info("End.");
            Console.ReadLine();
        }
    }
}
