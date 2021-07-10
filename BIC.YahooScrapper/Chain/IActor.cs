using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.YahooScrapper.Chain
{
    public interface IActor
    {
        bool Do(Context ctx);
        IActor Next { get; set; }
    }
}
