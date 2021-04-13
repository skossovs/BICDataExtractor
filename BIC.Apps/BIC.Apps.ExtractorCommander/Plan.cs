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
        // 1.     Implement skip ticker option. DONE.
        // 1.1.   for all scrappers if ticker fails, skip it or try again, or stop the app and create a file where to continue. >>
        // 1.2.   Create Excel UI that slices and dices report data by Sectors and Industries. Done
        // 1.2.1. Implement Average Ratios calculatons for Sectors and Industries. Done.
        // 2.     For Finviz KeyRatio need to Q := Q - 1, data is for previous quarter.Done
        // 3.     Create WPF UI
        // 3.1.   Command tree, yaml sectors, read the logs from the files. Start-Stop capability. Running indicator. Options to run windowless processes.
        // 4.     Reorganize tests.
        // 6.     Errors:
        // 6.1.   Merge fails on duplicates. The reason is companies late filling. Need to fix quarters.
        //        sometimes yearly data came instead of quarterly. >>
        // 6.1.2. Queue doesn't have enough time to process last item, before finished.
        // 6.1.3. Financial Sector cannot be scrapped. Need to debug

        // 7.     Implement Reports for different strategies. Next Levels.
        // 8.     Yahoo.Finance Scrapper for yearly
        // 9.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 10.     StackFrame.Debug instead of object name in logging
        // 11.    Block Chrome logs if possible.
    }
}
