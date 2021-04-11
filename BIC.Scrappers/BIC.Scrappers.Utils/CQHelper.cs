using BIC.Utils.Logger;
using BIC.Utils.Monads;
using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class CQHelper
    {
        public CQ InitiateWithContent(string htmlContent)
        {
            return CQ.CreateDocument(htmlContent);
        }
    }
}
