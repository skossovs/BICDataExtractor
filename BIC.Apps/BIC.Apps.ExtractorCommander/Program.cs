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

        static void Main(string[] args)
        {
            try
            {
                _logger.Info("Starting..");
                // TODO: implement commands
                var app = new CommandLineApplication();

                #region Option description
                app.HelpOption("--help");
                var optionResource = app.Option("-rs|--resource", "Resource"     , CommandOptionType.SingleValue);
                //var optionView     = app.Option("-vw|--view"    , "View"         , CommandOptionType.SingleValue); TODO: all views (objects) will be loaded
                var optionNoFilter = app.Option("-all|--All"    , "All no filter", CommandOptionType.NoValue);
                var optionSector   = app.Option("-sec|--sector" , "Sector"       , CommandOptionType.SingleValue);
                #endregion

                #region Executing Function definition
                app.OnExecute(() =>
                {
                    var resource = optionResource.HasValue() ? optionResource.Value() : null;
                    //var view     = optionView    .HasValue() ? optionView    .Value() : null;  TODO:
                    var sector   = optionSector  .HasValue() ? optionSector  .Value() : null;

                    if (optionResource.HasValue())
                    {
                        if (resource == Commands.Constants.FinvizResource)
                        {
                            if (optionNoFilter.HasValue())
                            {
                                Commands.Finviz.ScrapOverview(); // TODO: *** not finished
                                Commands.Finviz.ScrapFinancial();
                            }
                            else
                            {
                                Commands.Finviz.ScrapOverview();
                                Commands.Finviz.ScrapFinancial();
                            }
                        }
                    }
                    else
                    {
                        _logger.Error("Resource option is not specified: no extract can be done");
                        return -1; // TODO: constants for results are needed
                    }

                    return 0;
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
            Console.ReadLine();
        }
    }
}
