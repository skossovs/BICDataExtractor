using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ScrapperTests
{
    internal static class UtilsForTesting
    {
        private static object locker = new object();
        public static bool SetTheSettings()
        {
            lock (locker)
            {
                var settings = new Utils.SettingProcessors.AppSettingsProcessorLogger();
                bool result = settings.Populate();
                return result;
            }
        }
    }
}
