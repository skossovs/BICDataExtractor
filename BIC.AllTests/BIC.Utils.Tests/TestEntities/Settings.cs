using BIC.Utils.Attributes;
using BIC.Utils.SettingProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Tests.TestEntities
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

        public string    StringProperty        { get; set; }
        public DateTime  DateTimeProperty      { get; set; }
        public int       IntegerProperty       { get; set; }
        [Generic]
        public string    GenericStringProperty { get; set; }
        [Mandatory]
        public string    StringMandatoryProperty { get; set; }
    }
}
