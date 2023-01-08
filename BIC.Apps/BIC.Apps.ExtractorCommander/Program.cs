using BIC.Utils.Logger;
using Microsoft.Extensions.CommandLineUtils;
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
        private const  int  Success = 0;
        private const  int  Error   = -1;

        static void Main(string[] args)
        {
            try
            {
                _logger.Info("Starting..");
                var app = new CommandLineApplication();

                #region Command lines
                //  -rs finviz -etl secmaster                                 -- security master call
                //  -rs finviz -all                                           -- download all sectors
                //  -rs finviz -etl finance -sec healthcare                   -- download one sector
                //  -rs yahoo  -etl finance -sec healthcare                   -- download one sector
                //  -rs yahoo  -etl finance -sec "Consumer Defensive" -at DL  -- download one sector
                //  -rs yahoo  -etl finance -tkr BAC                          -- download one ticker
                //  -rs yahoo  -etl market  -tkr BAC                          -- download ticker's current price, option (if any)
                //  -rs fx
                #endregion

                #region Option description
                app.HelpOption("--help");  // TODO: implement help
                var optionResource         = app.Option("-rs|--resource"   , "Resource"          , CommandOptionType.SingleValue);
                //var optionView           = app.Option("-vw|--view"       , "View"              , CommandOptionType.SingleValue); TODO: all views (objects) will be loaded
                var optionFeed             = app.Option("-etl|--ETL"       , "ETL Feed type"     , CommandOptionType.SingleValue);
                var optionNoFilter         = app.Option("-all|--All"       , "All no filter"     , CommandOptionType.NoValue);
                var optionSector           = app.Option("-sec|--sector"    , "Sector"            , CommandOptionType.SingleValue);
                var optionStartAfterTicker = app.Option("-at|--afterticker", "Start after ticker", CommandOptionType.SingleValue);
                var optionSingleTicker     = app.Option("-tkr|--ticker"    , "Load single ticker", CommandOptionType.SingleValue);
                #endregion

                #region Executing Function definition
                app.OnExecute(() =>
                {
                    var resource = optionResource    .HasValue() ? optionResource    .Value() : null;
                    var sector   = optionSector      .HasValue() ? optionSector      .Value() : null;
                    var ticker   = optionSingleTicker.HasValue() ? optionSingleTicker.Value() : null;
                    // TODO: come up with bridge design pattern to properly combine commands, get rid of many-layered ifs
                    if (optionResource.HasValue())
                    {
                        if (resource == Commands.Constants.FinvizResource)
                        {
                            if (optionFeed.Value() == "secmaster")
                                Commands.Finviz.ScrapOverview();
                            else if (optionFeed.Value() == "finance")
                            {
                                if (optionNoFilter.HasValue())
                                    Commands.Finviz.ScrapFinancial();
                                else if (optionSector.HasValue())
                                    Commands.Finviz.ScrapFinancial(sector);
                                else
                                    throw new Exception("Neither -all nor sector specified command (-sec) was entered");
                            }
                        }
                        else if (resource == Commands.Constants.YahooResource)
                        {
                            if (optionFeed.Value() == "finance")
                            {
                                if (optionStartAfterTicker.HasValue())
                                {
                                    if (optionNoFilter.HasValue())
                                        Commands.Yahoo.ScrapTickersAfter(optionStartAfterTicker.Value());
                                    else if (optionSector.HasValue())
                                        Commands.Yahoo.ScrapTickersAfter(sector, optionStartAfterTicker.Value());
                                    else
                                        throw new Exception("Neither -all nor sector specified command (-sec) was entered");
                                }
                                else
                                {
                                    if (optionNoFilter.HasValue())
                                        Commands.Yahoo.ScrapTickers();
                                    else if (optionSector.HasValue())
                                        Commands.Yahoo.ScrapTickers(sector);
                                    else if (optionSingleTicker.HasValue())
                                        Commands.Yahoo.ScrapATicker(ticker);
                                    else
                                        throw new Exception("Neither -all nor sector specified command (-sec) was entered");
                                }
                            }
                        }
                        else if (resource == Commands.Constants.FxResource)
                        {
                            Commands.FxRates.Scrap();
                        }
                    }
                    else
                    {
                        _logger.Error("Resource option is not specified: no extract can be done");
                        return Error;
                    }

                    return Success;
                });
                #endregion

                var result = app.Execute(args); // TODO: return command result
            }
            catch (Exception ex)
            {
                _logger.Error("FATATL failure with message: " + ex.Message);
            }
            finally
            {
                _logger.Info(">>>>>> Finished Processing");
            }
#if   (DEBUG)
    Console.ReadLine();
#else
    _logger.Info("This was Release version");
#endif
        }
    }
}
