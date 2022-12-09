using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BIC.Foundation.Interfaces;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class ContentRetrieverFactory
    {
        private static IContentRetriever _chromeRetriever;
        private static IContentRetriever _yahooRetriever;
        public static IContentRetriever CreateInstance(ERetrieverType retrieverType)
        {
            IContentRetriever result = null;

            switch (retrieverType)
            {
                case ERetrieverType.Finviz:
                    if (_chromeRetriever == null)
                        _chromeRetriever = new HttpClientRetriever(new Delayers.VariableDelayer(), 1);
                    result = _chromeRetriever as IContentRetriever;
                    break;
                case ERetrieverType.Yahoo:
                    if (_yahooRetriever == null)
                        _yahooRetriever = new HttpClientRetriever(new Delayers.VariableDelayer(), 0);
                    result = _yahooRetriever as IContentRetriever;
                    break;
            }

            return result;
        }
    }
}
