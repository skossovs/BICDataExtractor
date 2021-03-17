using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public class LogServiceProvider
    {
        private static LogDelegator _logDelegator;

        public static ILog Logger
        {
            get
            {
                _logDelegator = _logDelegator ?? new LogDelegator();
                return _logDelegator as ILog;
            }
        }

        // This one is exotic way to call static destructor
        #region static destructor
        private sealed class Destructor
        {
            ~Destructor()
            {
                if(_logDelegator!=null)
                    _logDelegator.Dispose();
            }
        }
        private static readonly Destructor Finalise = new Destructor();
        #endregion
    }
}
