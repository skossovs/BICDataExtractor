using BIC.WPF.ScrapManager.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BIC.WPF.ScrapManager.MVVM
{
    /// <summary>
    /// Do not show log records that were generated before.
    /// </summary>
    public class LoggerViewModel : INotifyPropertyChanged
    {
        private long            _starting_index;
        private string          _processType;
        private string          _sbText;
        private Timer           _timer;
        private bool            _is_reading;
        private readonly string LOG_EXTRACTOR_PATH = Settings.GetInstance().ScrapperFileLogPath;
        private readonly string LOG_ETL_PATH       = Settings.GetInstance().EtlProcessFileLogPath;
        private string          _log_path;

        public LoggerViewModel()
        {
            _sbText = string.Empty;
            _is_reading = false;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStartMessage>(this, ReceiveStartCommand);
            // set timer
            _timer = new System.Timers.Timer(5000); // TODO: config
            _timer.Elapsed += ReadLogFile;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }
        public string ProcessType
        {
            get
            { return _processType; }
            set
            {
                _processType = value;

                if (_processType == "Scrapper")
                    _log_path = LOG_EXTRACTOR_PATH;
                else if (_processType == "ETL")
                    _log_path = LOG_ETL_PATH;

                // before working with file
                ReadLastIndex();
                _timer.Enabled = true;

                OnPropertyChanged("ProcessType");
            }
        }

        public string Text {
            get
            {
                return _sbText;
            }
            set
            {
                _sbText = value;
                OnPropertyChanged("Text");
            }
        }

        public void ReadLastIndex()
        {
            _starting_index = 0;
            if (!System.IO.File.Exists(_log_path))
                return;

            using (System.IO.FileStream fs = new System.IO.FileStream(_log_path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                _starting_index = fs.Length - 1;
            }
        }

        public void ReadLogFile(Object source, ElapsedEventArgs e)
        {
            if (_is_reading)
                return;

            if (!System.IO.File.Exists(_log_path))
                return;

            _is_reading = true;
            using (System.IO.FileStream fs = new System.IO.FileStream(_log_path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                long currentLength = fs.Length;
                long bufferLength  = currentLength - _starting_index - 1;
                if(bufferLength > 0)
                {
                    byte[] buffer = new byte[bufferLength];
                    fs.Seek(_starting_index, System.IO.SeekOrigin.Begin);
                    fs.Read(buffer, 0, (int)bufferLength);
                    Text = Encoding.UTF8.GetString(buffer);
                }
            }
            _is_reading = false;
        }

        ~LoggerViewModel()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void ReceiveStartCommand(ProcessStartMessage processStartMessage)
        {
            // once new process started it is needed to reset the index.
            ReadLastIndex();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
