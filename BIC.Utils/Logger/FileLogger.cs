using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    public class FileLogger : ILog, IDisposable
    {
        protected static StreamWriter _writer;
        // TODO: copy-paste
        private string _get_prefix(string pfxType)
        {
            // get calling method & type
            var stackTrace = new StackTrace();
            var methodName = stackTrace.GetFrame(4).GetMethod().Name;
            var className = stackTrace.GetFrame(4).GetMethod().ReflectedType.Name;
            return string.Format("{0} ({1}.{2}) {3}: "
                , pfxType
                , className
                , methodName
                , DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }

        static FileLogger()
        {
            _writer = new StreamWriter(Path.Combine(Settings.GetInstance().LogDirectory, "BIC.log"), true);
        }

        #region ILog
        public void Info(string message, params object[] p)
        {
            DisplayMessage(message, "INFO", p);
        }

        public void Warning(string message, params object[] p)
        {
            DisplayMessage(message, "WARN", p);
        }

        public void Debug(string message, params object[] p)
        {
            DisplayMessage(message, "DEBUG", p);
        }

        public void Error(string message, params object[] p)
        {
            DisplayMessage(message, "ERROR", p);
        }

        public void ReportException(Exception ex)
        {
            _writer.WriteLine(_get_prefix("EXCEPTION") + ex.Message);
            _writer.WriteLine(_get_prefix("STACK_TRACE") + ex.StackTrace);
            _writer.Flush();
        }
        #endregion

        private void DisplayMessage(string message, string typeMessage,  params object[] p)
        {
            if (p.Count() > 0)
                _writer.WriteLine(_get_prefix(typeMessage) + message, p);
            else
                _writer.WriteLine(_get_prefix(typeMessage) + message);
            _writer.Flush();
        }

        public void Dispose()
        {
            // TODO: define if it is closed, sometimes fails, just cover up for now
            try
            {
                _writer.Close();
            }
            catch(Exception ex)    { }
        }
    }
}
