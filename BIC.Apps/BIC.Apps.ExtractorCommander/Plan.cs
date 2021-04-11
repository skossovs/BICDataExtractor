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
        // 1.     Implement Reports for different strategies. >>
        // 1.2.   Create Excel UI that slices and dices report data by Sectors and Industries. >>
        // 1.2.1. Implement Average Ratios calculatons for Sectors and Industries. >>
        // 2.     For Finviz KeyRatio need to Q := Q - 1, data is for previous quarter.Done

        // 4.     Reorganize tests.
        // 6.     Errors:
        // 6.1.   Merge fails on duplicates. The reason is companies late filling. Need to fix quarters.>>
        // 6.1.2. Queue doesn't have enough time to process last item, before finished.
        // 6.1.3. Logger doesn't work for FileProcessor.

        // 7.     Yahoo.Finance Scrapper for yearly
        // 8.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 9.     StackFrame.Debug instead of object name in logging
        // 10.    Block Chrome logs if possible.
    }
}
