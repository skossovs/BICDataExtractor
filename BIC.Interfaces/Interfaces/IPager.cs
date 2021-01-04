using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    /// <summary>
    /// Define page metrics: records per page and how many pages returned
    /// when records per page is not regular or not defined, it is -1
    /// </summary>
    public interface IPager<TRequestParameters>  where TRequestParameters : class
    {
        bool DefineMetrics(TRequestParameters p, out int recordsPerPage , out int maxPage);
        IEnumerable<string> GenerateRequestAdresses();
    }
}
