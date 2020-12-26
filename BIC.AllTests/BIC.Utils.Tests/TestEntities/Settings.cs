using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Tests.TestEntities
{
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

        public string    StringProperty        { get; set; }
        public DateTime? DateTimeProperty      { get; set; }
        public int?      IntegerProperty       { get; set; }
        // Generic test properties
        public string    GenericStringProperty { get; set; }
    }
}
