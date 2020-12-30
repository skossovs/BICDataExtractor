using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    interface IPagerScrapper<T> : IScrapper<T>
    {
        IEnumerable<T> ScrapFromAllPages(IPager pager);
    }
}
