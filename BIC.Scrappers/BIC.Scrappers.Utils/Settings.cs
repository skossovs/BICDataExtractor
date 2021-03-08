using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;

namespace BIC.Scrappers.Utils
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
                Provider.PopulateProperties(_singletonSetings);
            }
            return _singletonSetings;
        }
        [Mandatory]
        public int TimeDelayInSeconds { get; set; }

        [Mandatory]
        public string ChromeLocation { get; set; }

    }
}
