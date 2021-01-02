using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Utils.Monads;

namespace BIC.Scrappers.FinvizScrapper
{
    public static class Conversions
    {
        public static HttpRequestData FromFinvizParametersToHttpRequestData(FinvizParameters p)
        {
            var hr = new HttpRequestData();

            hr.Filters = new HttpRequestData.Filter()
            {
                Country  = p.Filters.CountrFilter,
                Exchange = p.Filters.ExchangeFilter,
                Index    = p.Filters.IndexFilter,
                Industry = p.Filters.IndustryFilter,
                Sector   = p.Filters.SectorFilter
            };

            hr.FilterView = (BIC.Utils.Conversions.EnumToIntObj(p.FilterView)).With(e => Convert.ToString(e));
            hr.View = Convert.ToString(BIC.Utils.Conversions.EnumToIntObj(p.View)).With(e => Convert.ToString(e));

            return hr;
        }
    }
}
