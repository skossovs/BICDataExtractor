using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public class SimpleLogger : ILog
    {
        public SimpleLogger()   {}

        private string _get_prefix(string pfxType)
        {
            // get calling method & type
            var stackTrace = new StackTrace();
            var methodName = stackTrace.GetFrame(2).GetMethod().Name;
            var className = stackTrace.GetFrame(2).GetType().Name;
            return string.Format("{0} ({1}.{2}) {3}: "
                , pfxType
                , className
                , methodName
                , DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        public void Info(string message, params object[] p)
        {
            Console.WriteLine(_get_prefix("INFO") + message, p);
        }

        public void Warning(string message, params object[] p)
        {
            Console.WriteLine(_get_prefix("WARN") + message, p);
        }

        public void Debug(string message, params object[] p)
        {
            Console.WriteLine(_get_prefix("DEBUG") + message, p);
        }

        public void Error(string message, params object[] p)
        {
            Console.WriteLine(_get_prefix("ERROR") + message, p);
        }

        public void ReportException(Exception ex)
        {
            Console.WriteLine(_get_prefix("EXCEPTION") + ex.Message);
            Console.WriteLine(_get_prefix("STACK_TRACE") + ex.StackTrace);
        }
    }
}
