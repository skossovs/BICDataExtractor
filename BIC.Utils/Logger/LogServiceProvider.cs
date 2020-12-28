using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public class LogServiceProvider
    {
        private static ILog _logInstance;

        public static ILog Logger
        {
            get
            {
                _logInstance = _logInstance ?? new SimpleLogger();
                return _logInstance;
            }
            set { _logInstance = value; }
        }
    }
}
