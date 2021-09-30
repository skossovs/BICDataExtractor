﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    // This class is for planning only doesn't do anything, just thoughts in here
    class Plan
    {
        // 1.     Create WPF UI >>
        // 1.1.   Make stoppable yahoo
        // 1.2.   Design is still ugly.
        // 1.3.   Make Yahoo Feed stoppable. >>
        // 1.4.   Stoppable ETL process. DONE
        // 1.5.   Expand tree view dynamically.
        // 2.     Reorganize tests.
        // 3.     Errors:
        // 3.1.   Merge fails on duplicates. The reason is companies late filling. Need to fix quarters.
        // 3.2.   Queue doesn't have enough time to process last item, before finished.
        // 3.3.   Before apply FX Rates search for "Currency in CNY.All numbers in thousands" string. Sometimes foreign companies post fundamentals in dollars.
        // 3.4.   fx loading shows no progress.
        // 3.5.   StackFrame.Debug instead of object name in logging. FIXED
        // 4.     Implement Report for yearly data.
        // 5.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 6.     Block Chrome logs if possible.
    }
}
