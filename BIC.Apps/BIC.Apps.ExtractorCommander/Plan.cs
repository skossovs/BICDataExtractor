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
        // 1.     Implement already loaded tickers exclusion. DONE.
        // 2.     Exclude ETFs from fundamentals loading.
        // 3.     Create WPF UI
        // 3.1.   Design is still ugly.
        // 3.2.   Reflect log in UI.
        // 4.     Reorganize tests.

        // 6.     Errors:
        // 6.1.   Merge fails on duplicates. The reason is companies late filling. Need to fix quarters.
        // 6.1.1. sometimes yearly data came instead of quarterly. DONE.
        // 6.1.2. Queue doesn't have enough time to process last item, before finished.
        // 6.1.3. Financial Sector cannot be scrapped. Need to debug. DONE.
        // 6.2.   Populate field Earnings Date in Finviz extract. There is a dirt in Date field. DONE.
        // 6.3.   Before apply FX Rates search for "Currency in CNY.All numbers in thousands" string. Sometimes foreign companies post fundamentals in dollars.
        // 6.4.    StackFrame.Debug instead of object name in logging
        // 7.     Implement Report for yearly data.
        // 9.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 10.    Block Chrome logs if possible.
    }
}
