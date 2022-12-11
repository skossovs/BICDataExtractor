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
    ///  analyse the log, parse it and notify UI
    /// </summary>
    public class UIStatusLogger : ILog
    {
        private ILog _originalLogger;
        private bool _hasError;
        private SenderReciever<CommandMessage, StatusMessage> _senderReceiver;
        public UIStatusLogger(ILog originalLogger, SenderReciever<CommandMessage, StatusMessage> senderReceiver)
        {
            _originalLogger = originalLogger;
            _senderReceiver = senderReceiver;
            _hasError = false;
        }

        public void Debug(string message, params object[] p)
        {
            _originalLogger.Debug(message, p);
        }

        public void Error(string message, params object[] p)
        {
            _hasError = true;
            _senderReceiver.Send(new StatusMessage()
            {
                ChannelID = 0, // TODO: set up the proper channel
                ProcessStatus = Foundation.Interfaces.EProcessStatus.Killed
            });
            _originalLogger.Error(message, p);
        }

        public void Info(string message, params object[] p)
        {
            if (message == "#Finished" && !_hasError)
                _senderReceiver.Send(new StatusMessage()
                {
                    ChannelID = 0, // TODO: set up the proper channel
                    ProcessStatus = Foundation.Interfaces.EProcessStatus.Finished
                });
            else if (message == "#Stopped")
                _senderReceiver.Send(new StatusMessage()
                {
                    ChannelID = 0, // TODO: set up the proper channel
                    ProcessStatus = Foundation.Interfaces.EProcessStatus.Stopped
                });
            else if (message == "#Running")
                _senderReceiver.Send(new StatusMessage()
                {
                    ChannelID = 0, // TODO: set up the proper channel
                    ProcessStatus = Foundation.Interfaces.EProcessStatus.Running
                });

            _originalLogger.Info(message, p);
        }

        public void ReportException(Exception ex)
        {
            _hasError = true;
            _senderReceiver.Send(new StatusMessage()
            {
                ChannelID = 0, // TODO: set up the proper channel
                ProcessStatus = Foundation.Interfaces.EProcessStatus.Killed
            });

            _originalLogger.ReportException(ex);
        }

        public void Warning(string message, params object[] p)
        {
            _originalLogger.Warning(message, p);
        }
    }
}
