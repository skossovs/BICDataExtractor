using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    class Program
    {
        private static ILog _logger = LogServiceProvider.Logger;
        static int Main(string[] args)
        {
            StringBuilder sbCommandLine = new StringBuilder();
            for(int i = 0; i < args.Count(); i++)
            {
                sbCommandLine.AppendFormat(" {0}", args[i]);
            }

            _logger.Info("Command Parameters {0}", sbCommandLine.ToString());

            try
            {
                _logger.Info("Start Processing..");
                var strategy = Strategy.StrategyManager.SetupStrategy(args);
                strategy.Execute();
                _logger.Info("End Processing SUCCESSFULLY..");
            }
            catch(Exception ex)
            {
                _logger.Error("End Processing with ERRORS..");
                _logger.ReportException(ex);
                return (int)BIC.Foundation.Interfaces.ProcessResult.FATAL;
            }

            return (int) BIC.Foundation.Interfaces.ProcessResult.SUCCESS;
        }
    }
}
