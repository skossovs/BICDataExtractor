using System;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    public abstract class ScrapCommand
    {
        /*
                #region Option description
                CommandLineApplication app = new CommandLineApplication();

            resource
                CommandOption optionResource         = app.Option("-rs|--resource", "Resource"     , CommandOptionType.SingleValue);
                //var optionView           = app.Option("-vw|--view"    , "View"         , CommandOptionType.SingleValue); TODO: all views (objects) will be loaded
            feed-type
                var optionFeed             = app.Option("-etl|--ETL"    , "ETL Feed type", CommandOptionType.SingleValue);
            filter
                var optionNoFilter         = app.Option("-all|--All"    , "All no filter", CommandOptionType.NoValue);
                var optionSector           = app.Option("-sec|--sector" , "Sector"       , CommandOptionType.SingleValue);
            after ticker
                var optionStartAfterTicker = app.Option("-at|--afterticker", "Start after ticker", CommandOptionType.SingleValue);
            one ticker

                #endregion

        */

        public IBridgeComponents _iBridgeComponents;
        public abstract void Scrap<T>() where T : class, new();
    }
}
