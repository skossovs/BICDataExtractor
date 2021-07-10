using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    [AppSettingsXML]
    class Settings
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
        public string InputDirectory { get; set; }
        [Mandatory]
        public string SQLConnectionString { get; set; }
        [Mandatory]
        public string ZipFileName { get; set; }
        [Mandatory]
        public int   ZipMaxFileLength { get; set; }
    }
}
