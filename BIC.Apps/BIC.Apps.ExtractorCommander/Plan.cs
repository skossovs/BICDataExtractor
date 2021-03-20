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
        // 3. Create project that export files in SQL database. >>
        // 3.1. Create File Manager Concept DONE
        // 3.2. Create Security Master. Must provide Merge operation always. DONE
        // 4. Block Chrome logs if possible.
        // 5. Implement variable timedelay.
        // 5.1. Implement delay skip if different site detected. So Yahoo doesn't wait for Finviz.
        // 5.2. as better alternative to 5.1. make request class non-static, create instance for each site and make it behave differently
        // 6. Create Yahoo.Finance Scrapper library
        // 7. Reorganize tests.

    }
}
