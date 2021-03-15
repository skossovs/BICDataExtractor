using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.Logger
{
    internal class LogDelegator : ILog, IDisposable
    {
        private List<ILog> _listeners;
        public LogDelegator()
        {
            _listeners = new List<ILog>() { new SimpleLogger() };

            if (Settings.GetInstance().UseFileLog.YNtoBool())
                _listeners.Add(new FileLogger());

        }
        public void Debug(string message, params object[] p)
        {
            foreach(var listener in _listeners)
            {
                listener.Debug(message, p);
            }
        }

        public void Error(string message, params object[] p)
        {
            foreach (var listener in _listeners)
            {
                listener.Error(message, p);
            }
        }

        public void Info(string message, params object[] p)
        {
            foreach (var listener in _listeners)
            {
                listener.Info(message, p);
            }
        }

        public void Warning(string message, params object[] p)
        {
            foreach (var listener in _listeners)
            {
                listener.Warning(message, p);
            }
        }
        public void ReportException(Exception ex)
        {
            foreach (var listener in _listeners)
            {
                listener.ReportException(ex);
            }
        }

        public void Dispose()
        {
            _listeners.ForEach(l => (l as IDisposable).Dispose());
        }
    }
}
