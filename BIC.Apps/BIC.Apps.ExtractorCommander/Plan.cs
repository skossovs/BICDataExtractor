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
        // 1.     Use postman to update google chrome.exe component. Instead of dragging crome.exe with every project REFER TO IT:
        //        C:\Program Files (x86)\Google\Chrome\Application
        // 2.     Create WPF UI
        // 2.1.   Design is still ugly.
        // 2.2.   Expand tree view dynamically.

        // 2.     Reorganize tests.

        // 3.     Errors: >>
        // 3.2.   Queue doesn't have enough time to process last item, before finished.
        // 3.3.   Before apply FX Rates search for "Currency in CNY.All numbers in thousands" string. Sometimes foreign companies post fundamentals in dollars.
        // 3.4.   fx loading shows no progress.
        // 3.5.   in WPF & MSMQ exe statuses are not properly signalled.
        // 5.     Fix linq2db issue with connection object. Specifically for Merge. Looks like Merge operation happens after connection object is closed.
        // 6.     Block Chrome logs if possible.
    }
}
