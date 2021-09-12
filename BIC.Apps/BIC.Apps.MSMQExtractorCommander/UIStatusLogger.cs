using BIC.Apps.MSMQExtractorCommander.MSMQData;
using BIC.Utils.Logger;
using BIC.Utils.MSMQ;
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
        private SenderReciever<CommandMessage, StatusMessage> _senderReceiver;
        public UIStatusLogger(ILog originalLogger, SenderReciever<CommandMessage, StatusMessage> senderReceiver)
        {
            _originalLogger = originalLogger;
            _senderReceiver = senderReceiver;
        }

        public void Debug(string message, params object[] p)
        {
            // TODO: if parser found the replica it needs _senderReceiver.Send()
            throw new NotImplementedException();
            _originalLogger.Debug(message, p);
        }

        public void Error(string message, params object[] p)
        {
            // TODO: once error signaled, don't send anymore statuses, stop the process
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
