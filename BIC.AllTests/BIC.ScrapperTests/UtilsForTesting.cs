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
        private static bool IsInitializedAlready;
        public static bool SetTheSettings()
        {
            lock (locker)
            {
                if (IsInitializedAlready)
                    return true;

                IsInitializedAlready = true;
                var settings = new Utils.SettingProcessors.AppSettingsProcessorLogger();
                bool result = settings.Populate();
                return result;
            }
        }
    }
}
