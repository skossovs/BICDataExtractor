using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    // This class is for planning only doesn't do anything, just thoughts in here
    class Plan
    {
        // 2.     Create WPF UI
        // 2.1.   Design is still ugly.
        // 2.2.   Expand tree view dynamically.
        // 2.     Reorganize tests.

        // 3.     Errors: >>
        // 3.3.   Before apply FX Rates search for "Currency in CNY.All numbers in thousands" string. Sometimes foreign companies post fundamentals in dollars.
        // 3.4.   fx loading shows no progress. DISMISSED.
        // 3.5.   in WPF & MSMQ exe statuses are not properly signalled when error happened the running status changes.
        // 5.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed. DONE.
        // 8.     Dowload Watched options
        // 8.1.   Implement UI tab.
        // 8.2.   Implement Option feed from yahoo
        // 9.     Get rid of finviz completely and calculate all ratios from yahoo fundamentals
    }
}
