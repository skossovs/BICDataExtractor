﻿using System;
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
        private static ChromeRetriever _yahooRetriever;
        private static SimpleRetriever _simpleRetriever;
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
                case ERetrieverType.Yahoo:
                    if (_yahooRetriever == null)
                        _yahooRetriever = new ChromeRetriever(new Delayers.VariableDelayer());
                    result = _yahooRetriever as IContentRetriever;
                    break;
                case ERetrieverType.Simple:
                    if (_simpleRetriever == null)
                        _simpleRetriever = new SimpleRetriever(new Delayers.VariableDelayer());
                    result = _simpleRetriever as IContentRetriever;
                    break;
            }

            return result;
        }
    }
}
