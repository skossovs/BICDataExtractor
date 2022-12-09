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
        private static IContentRetriever _finvizRetriever;
        private static IContentRetriever _yahooRetriever;
        private static IContentRetriever _moneyConverterRetriever;
        public static IContentRetriever CreateInstance(ERetrieverType retrieverType)
        {
            IContentRetriever result = null;

            switch (retrieverType)
            {
                case ERetrieverType.Finviz:
                    if (_finvizRetriever == null)
                        _finvizRetriever = new HttpClientRetriever(new Delayers.VariableDelayer(), 1);
                    result = _finvizRetriever as IContentRetriever;
                    break;
                case ERetrieverType.Yahoo:
                    if (_yahooRetriever == null)
                        _yahooRetriever = new HttpClientRetriever(new Delayers.VariableDelayer(), 0);
                    result = _yahooRetriever as IContentRetriever;
                    break;
                case ERetrieverType.MoneyConverter:
                    if (_moneyConverterRetriever == null)
                        _moneyConverterRetriever = new ChromeRetriever(new Delayers.VariableDelayer());
                    result = _moneyConverterRetriever as IContentRetriever;
                    break;
            }

            return result;
        }
    }
}
