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
        // 1. Fix Chrome memory leak. Cut off chrome.exe process mad respawn. DONE
        // 2. Implement file logging. DONE
        // 2.1. Settings has hard time to accomodate in BIC.Utils project. File Logging require Log path. DONE
        // 2.2. Simple Logger and File Logger places in hierarchy are not clear. DONE
        // 3. Create project that export files in SQL database
        // 3.1. Create Security Master.
        // 4. Block Chrome logs if possible.
        // 5. Create DB ETL project.
        // 6. Implement variable timedelay.
        // 6.1. Implement delay skip if different site detected. So Yahoo doesn't wait for Finviz.
        // 7. Create Yahoo.Finance Scrapper library
        // 8. Reorganize tests.

    }
}
