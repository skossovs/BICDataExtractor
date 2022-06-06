using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;

namespace BIC.NasdaqLiveScrapper
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
        public string UrlRoot { get; set; }
        public int? DefaultDocumentsCount { get; set; }
        [Mandatory]
        public string OutputDirectory { get; set; }
    }
}
