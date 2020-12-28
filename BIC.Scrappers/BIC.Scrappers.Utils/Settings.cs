using BIC.Utils.Attributes;

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
                _singletonSetings = new Settings();
            return _singletonSetings;
        }
        [Mandatory]
        public int TimeDelayInSeconds { get; set; }
    }
}
