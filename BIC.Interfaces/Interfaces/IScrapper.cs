using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Foundation.Interfaces
{
    /*
     * 1. Find the fragment incorporating all data
     * 2. Find the fragments incorporating single records
     * 3. Call Parser and create list of objects
     */
    public interface IScrapper<T>
    {
        string FindRawContent(string pageContent);
        IEnumerable<string> SeparateIntoRecordsContent(string rawContent);
        IEnumerable<T> CallParsers(string parsingFragment);
    }
}
