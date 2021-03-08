using BIC.Utils.SettingProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.SettingProcessors
{
    public static class Provider
    {
        public static void PopulateProperties(object singletonSetings)
        {
            var settingHelper = new AppSettingsProcessorLogger();
            settingHelper.Populate(singletonSetings, singletonSetings.GetType().Assembly);
        }
    }
}
