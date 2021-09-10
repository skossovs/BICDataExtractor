using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    /// <summary>
    /// TODO: analyse the log, parse it and notify UI
    /// </summary>
    public class UIStatusLogger : ILog
    {
        private ILog _originalLogger;
        public UIStatusLogger(ILog originalLogger)
        {
            _originalLogger = originalLogger;
        }

        public void Debug(string message, params object[] p)
        {
            throw new NotImplementedException();
            _originalLogger.Debug(message, p);
        }

        public void Error(string message, params object[] p)
        {
            throw new NotImplementedException();
            _originalLogger.Error(message, p);
        }

        public void Info(string message, params object[] p)
        {
            throw new NotImplementedException();
            _originalLogger.Info(message, p);
        }

        public void ReportException(Exception ex)
        {
            throw new NotImplementedException();
            _originalLogger.ReportException(ex);
        }

        public void Warning(string message, params object[] p)
        {
            throw new NotImplementedException();
            _originalLogger.Warning(message, p);
        }
    }
}
