using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;

namespace BIC.Apps.MSMQEtlProcess
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
        // TODO: drop it
        //[Mandatory]
        //public string ScrapperFilePath { get; set; }

        //[Mandatory]
        //public string EtlProcessFilePath { get; set; }
        //[Mandatory]
        //public string ScrapperFileLogPath { get; set; }

        //[Mandatory]
        //public string EtlProcessFileLogPath { get; set; }

        [Mandatory]
        public string MsmqNameStatusEtl { get; set; }

        [Mandatory]
        public string MsmqNameCommands { get; set; }

        [Mandatory]
        public int SleepTimeMsmqReadMsec { get; set; }
    }
}
