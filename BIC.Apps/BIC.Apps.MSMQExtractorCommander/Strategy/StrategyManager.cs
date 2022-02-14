using System;
using BIC.Utils.Logger;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Scrappers.FinvizScrapper;
using BIC.Apps.MSMQExtractorCommander.MSMQData;
using BIC.Foundation.Interfaces;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public static class StrategyManager
    {
        private static ILog _logger = LogServiceProvider.Logger;

        public static IStrategy SetupStrategy(string[] args, Utils.MSMQ.SenderReciever<CommandMessage, StatusMessage> mq)
        {
            IStrategy strategy = null;
            StrategyParameters strategyParameters = new StrategyParameters();
            try
            {
                _logger.Info("Parsing command and set up strategy..");
                var app = new CommandLineApplication();

                #region Command lines
                //  -rs finviz -etl secmaster                                 -- security master call
                //  -rs finviz -all                                           -- download all sectors
                //  -rs finviz -etl finance -sec healthcare                   -- download one sector
                //  -rs yahoo  -etl finance -sec healthcare                   -- download one sector
                //  -rs yahoo  -etl finance -sec "Consumer Defensive" -at DL  -- download one sector
                //  -rs yahoo  -etl finance -tkr BAC                          -- download one ticker
                //  -rs fx
                #endregion

                #region Option description
                app.HelpOption("--help");  // TODO: implement help
                var optionResource = app.Option("-rs|--resource", "Resource", CommandOptionType.SingleValue);
                var optionFeed = app.Option("-etl|--ETL", "ETL Feed type", CommandOptionType.SingleValue);
                var optionNoFilter = app.Option("-all|--All", "All no filter", CommandOptionType.NoValue); // TODO: drop it
                var optionSector = app.Option("-sec|--sector", "Sector", CommandOptionType.SingleValue);
                var optionStartAfterTicker = app.Option("-at|--afterticker", "Start after ticker", CommandOptionType.SingleValue);
                var optionSingleTicker = app.Option("-tkr|--ticker", "Load single ticker", CommandOptionType.SingleValue);
                #endregion

                #region Executing Function definition
                app.OnExecute(() =>
                {
                    var resource = optionResource.HasValue() ? optionResource.Value() : null;
                    IStoppableStatusable<ILog> stoppableStatusable = new StoppableStatusable(mq);
                    // process filters first
                    strategyParameters.Sector   = optionSector.HasValue()           ? optionSector.Value()           : null;
                    strategyParameters.TickerAt = optionStartAfterTicker.HasValue() ? optionStartAfterTicker.Value() : null;

                    if (optionResource.HasValue())
                    {
                        if (resource == Constants.FinvizResource)
                        {
                            if (optionFeed.Value() == "secmaster")
                                strategy = new SecMasterStrategy(strategyParameters, stoppableStatusable) as IStrategy;
                            else if (optionFeed.Value() == "finance")
                                strategy = new KeyRatiosStrategy(strategyParameters, stoppableStatusable) as IStrategy;
                        }
                        else if (resource == Constants.YahooResource)
                        {
                            if (optionFeed.Value() == "finance")
                                strategy = new BICStrategy(strategyParameters, stoppableStatusable) as IStrategy;
                        }
                        else if (resource == Constants.FxResource)
                        {
                            strategy = new FXRatesStrategy() as IStrategy;
                        }
                    }
                    else
                    {
                        throw new Exception("Resource option is not specified: no extract can be done");
                    }

                    return 0;
                });
                #endregion

                app.Execute(args);
            }
            catch (Exception ex)
            {
                _logger.Error("FATATL failure while strategy initialization: ");
                _logger.ReportException(ex);
                throw;
            }

            return strategy;
        }
    }
}
