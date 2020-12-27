using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
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
        public string UrlRoot { get; set; }
        public int? DefaultDocumentsCount { get; set; }
    }
}
