using BIC.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class HeaderScrapper : IScrapper<string>
    {
        public IEnumerable<string> CallParsers(string parsingFragment)
        {
            throw new NotImplementedException();
        }

        public string FindRawContent(string pageContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> SeparateIntoRecordsContent(string rawContent)
        {
            throw new NotImplementedException();
        }
    }
}
