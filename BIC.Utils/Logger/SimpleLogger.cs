using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public class SimpleLogger : ILog, IDisposable
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (p.Count() > 0)
                Console.WriteLine(_get_prefix("INFO") + message, p);
            else
                Console.WriteLine(_get_prefix("INFO") + message);
            Console.ResetColor();
        }

        public void Warning(string message, params object[] p)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (p.Count() > 0)
                Console.WriteLine(_get_prefix("WARN") + message, p);
            else
                Console.WriteLine(_get_prefix("WARN") + message);
            Console.ResetColor();
        }

        public void Debug(string message, params object[] p)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if(p.Count() > 0)
                Console.WriteLine(_get_prefix("DEBUG") + message, p);
            else
                Console.WriteLine(_get_prefix("DEBUG") + message);
            Console.ResetColor();
        }

        public void Error(string message, params object[] p)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (p.Count() > 0)
                Console.WriteLine(_get_prefix("ERROR") + message, p);
            else
                Console.WriteLine(_get_prefix("ERROR") + message);
            Console.ResetColor();
        }

        public void ReportException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(_get_prefix("EXCEPTION") + ex.Message);
            Console.WriteLine(_get_prefix("STACK_TRACE") + ex.StackTrace);
            Console.ResetColor();
        }

        public void Dispose()
        {
        }
    }
}
