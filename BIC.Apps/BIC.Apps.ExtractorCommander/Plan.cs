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
        // 3.     Create WPF UI
        // 3.1.   Command tree, yaml sectors, read the logs from the files. Start-Stop capability. Running indicator. Options to run windowless processes. DONE.
        // 3.2.   Finish up MSMQ recieval. DONE.
        // 3.3.   Design is still ugly.
        // 3.4.   Reflect log in UI.
        // 4.     Reorganize tests.

        // 6.     Errors:
        // 6.1.   Merge fails on duplicates. The reason is companies late filling. Need to fix quarters.
        //        sometimes yearly data came instead of quarterly.
        // 6.1.2. Queue doesn't have enough time to process last item, before finished.
        // 6.1.3. Financial Sector cannot be scrapped. Need to debug
        // 6.2.   Populate field Earnings Date in Finviz extract. There is a dirt in Date field.

        // 7.     Implement Reports for different strategies. Next Levels.
        // 8.     Yahoo.Finance Scrapper for yearly
        // 9.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 10.    StackFrame.Debug instead of object name in logging
        // 11.    Block Chrome logs if possible.
    }
}
