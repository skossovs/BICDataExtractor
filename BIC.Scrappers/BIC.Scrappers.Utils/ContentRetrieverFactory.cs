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
        private static ChromeRetriever _chromeRetriever;
        public static IContentRetriever CreateInstance(ERetrieverType retrieverType)
        {
            IContentRetriever result = null;

            switch (retrieverType)
            {
                case ERetrieverType.Finviz:
                    if (_chromeRetriever == null)
                        _chromeRetriever = new ChromeRetriever(new Delayers.VariableDelayer());
                    result = _chromeRetriever as IContentRetriever;
                    break;
            }

            return result;
        }
    }
}
