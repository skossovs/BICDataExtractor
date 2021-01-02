using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    interface IPagerScrapper<T, TRequestParameters> : IScrapper<T> where TRequestParameters : class
    {
        IEnumerable<T> ScrapFromAllPages(IPager<TRequestParameters> pager);
    }
}
