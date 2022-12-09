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

        /// <summary>
        ///  environment dependent properties here. Storing the template only, release debug will be chosen based on environment.
        ///  environment key |ENV|
        /// </summary>
        private string _scrapperFilePathTemplate;
        private string _etlProcessFilePathTemplate;
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
        public string ScrapperFilePath {
            get
            {
                string scrapperFilePath = _scrapperFilePathTemplate;
#if DEBUG
                scrapperFilePath = _scrapperFilePathTemplate.Replace("|ENV|", "Debug");
#else
                scrapperFilePath = _scrapperFilePathTemplate.Replace("|ENV|", "Release");
#endif
                return scrapperFilePath;
            }
            set
            {
                _scrapperFilePathTemplate = value;
            }
        }

        [Mandatory]
        public string EtlProcessFilePath
        {
            get
            {
                string etlProcessFilePath = _etlProcessFilePathTemplate;
#if DEBUG
                etlProcessFilePath = _etlProcessFilePathTemplate.Replace("|ENV|", "Debug");
#else
                etlProcessFilePath = _etlProcessFilePathTemplate.Replace("|ENV|", "Release");
#endif
                return etlProcessFilePath;
            }
            set
            {
                _etlProcessFilePathTemplate = value;
            }
        }
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
