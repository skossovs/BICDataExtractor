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
        // 1. Implement Reports for different strategies.
        // 1.1. Create ETL procedure, that joins KeyRatio with BIC data.
        // 1.2. Create Excel UI that slices and dices report data by Sectors and Industries
        // 1.2.1. Implement Average Ratios calculatons for Sectors and Industries
        // 2. Yahoo.Finance Scrapper for yearly
        // 3. Implement File archivarius
        // 4. Reorganize tests.

        // 8. Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 9. StackFrame.Debug instead of object name in logging
        // 10. Block Chrome logs if possible.
    }
}
