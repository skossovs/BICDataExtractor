using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    [AppSettingsXML]
    public class Settings
    {
        private static Settings _singletonSetings;

        private Settings() { }
        public static Settings GetInstance()
        {
            if (_singletonSetings == null)
            {
                _singletonSetings = new Settings();
                var settingHelper = new AppSettingsProcessorLogger(); // TODO: base class is needed
                settingHelper.Populate(_singletonSetings, _singletonSetings.GetType().Assembly);
            }
            return _singletonSetings;
        }
        [Mandatory]
        public string UrlRoot             { get; set; }
        public int? DefaultDocumentsCount { get; set; }
        [Mandatory]
        public string OutputDirectory     { get; set; }
    }
}
