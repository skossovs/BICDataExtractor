using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager
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

        [Mandatory, EnvironmentDependent]
        public string ScrapperFilePath { get; set; }

        [Mandatory, EnvironmentDependent]
        public string EtlProcessFilePath { get; set; }

        [Mandatory]
        public string ScrapperFileLogPath { get; set; }

        [Mandatory]
        public string EtlProcessFileLogPath { get; set; }

        [Mandatory]
        public string MsmqNameStatusEtl { get; set; }

        [Mandatory]
        public string MsmqNameStatusScrap { get; set; }

        [Mandatory]
        public string MsmqNameCommands { get; set; }

        [Mandatory]
        public int SleepTimeMsmqReadMsec { get; set; }

        [Mandatory]
        public int LogRefreshIntervalMsec { get; set; }
    }
}
