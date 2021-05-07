using BIC.WPF.ScrapManager.Common;
using BIC.WPF.ScrapManager.MVVM.Messages;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.WPF.ScrapManager.Data;
using BIC.Foundation.Interfaces;

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
        private string _processType;

        public ControlProcessViewModel()
        {
            _startCommand = new DelegateCommand<string>(
                (arguments) =>
                {
                    var e = (EProcessType)Enum.Parse(typeof(EProcessType), this.ProcessType);
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStartMessage(arguments, e));
                });
            _stopCommand = new DelegateCommand<string>(
                (_) =>
                {
                    var e = (EProcessType)Enum.Parse(typeof(EProcessType), this.ProcessType);
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStopMessage(e));
                });
        }

        public string ProcessType
        {
            get
            {
                return _processType;
            }
            set
            {
                _processType = value;
                OnPropertyChanged("ProcessType");
            }
        }

        public DelegateCommand<string> StartCommand
        {
            get { return _startCommand; }
        }

        public DelegateCommand<string> StopCommand
        {
            get { return _stopCommand; }
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
