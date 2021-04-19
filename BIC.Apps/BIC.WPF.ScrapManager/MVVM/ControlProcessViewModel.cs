using BIC.WPF.ScrapManager.Common;
using BIC.WPF.ScrapManager.MVVM.Messages;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM
{
    /// <summary>
    /// TODO:
    /// 1. Start/Stop windows process
    /// 2. Accept process parameters
    /// 3. Provide result of process ending
    /// 4. Transfer statuses from running process
    /// </summary>
    public class ControlProcessViewModel : INotifyPropertyChanged
    {
        private DelegateCommand<string> _startCommand;
        private DelegateCommand<string> _stopCommand;
        private string _processFilePath;

        public ControlProcessViewModel()
        {
            _startCommand = new DelegateCommand<string>(
                (arguments) =>
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStartMessage(this.ProcessFilePath, arguments));
                });
            _stopCommand = new DelegateCommand<string>(
                (_) =>
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStopMessage(this.ProcessFilePath));
                });

        }

        public string ProcessFilePath
        {
            get
            {
                return _processFilePath;
            }
            set
            {
                _processFilePath = value;
                OnPropertyChanged("ProcessFilePath");
            }
        }

        public DelegateCommand<string> StartCommand
        {
            get { return _startCommand; }
        }

        public DelegateCommand<string> StopCommand
        {
            get { return _startCommand; }
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
