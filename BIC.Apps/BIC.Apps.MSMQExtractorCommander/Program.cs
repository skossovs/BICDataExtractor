using BIC.Apps.MSMQExtractorCommander.MSMQData;
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
                // TODO: Settings
                using (var mq = new Utils.MSMQ.SenderReciever<CommandMessage, StatusMessage>(".\\Private$\\bic-commands", ".\\Private$\\bic-status-scrap", 200))
                {
                    mq.StartWatching();
                    _logger.Info("Start Processing..");
                    var strategy = Strategy.StrategyManager.SetupStrategy(args, mq);
                    strategy.Execute();
                    mq.StopWatching();
                }
            }
            catch(Exception ex)
            {
                _logger.Error("End Processing with ERRORS..");
                _logger.ReportException(ex);
                return (int)BIC.Foundation.Interfaces.ProcessResult.FATAL;
            }

            _logger.Info("End Processing SUCCESSFULLY..");
            return (int) BIC.Foundation.Interfaces.ProcessResult.SUCCESS;
        }
    }
}
