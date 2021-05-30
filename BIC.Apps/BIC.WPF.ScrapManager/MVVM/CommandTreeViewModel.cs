using BIC.WPF.ScrapManager.Common;
using BIC.WPF.ScrapManager.Data;
using BIC.WPF.ScrapManager.MVVM.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BIC.WPF.ScrapManager.MVVM
{
    class CommandTreeViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private CommandCache _content;
        private List<Group>  _treeViewContentRepresentation;


        public CommandTreeViewModel()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<CommandCacheFileMessage>(this, ReceiveFileCommand);
            // TODO: Temporary send reload message to itself
            SendLoadFileMessage();
        }

        // TODO: drop it
        private void SendLoadFileMessage()
        {
            var m = new CommandCacheFileMessage("OPEN", "CommandFile.yaml");
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(m);
        }

        private void ReceiveFileCommand(CommandCacheFileMessage commandCacheFileMessage)
        {
            try
            {
                switch (commandCacheFileMessage.Command)
                {
                    case "OPEN":
                        _content = (CommandCache)ApplicationCommands.ReadYamlObject(commandCacheFileMessage.Path, typeof(CommandCache));
                        if (_content != null)
                            TreeData = ConvertFromCommandCacheToGroups();
                        break;
                    default:
                        throw new Exception($"Unknnown File command {commandCacheFileMessage.Command}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message
                    , "Failed To Process File Command"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Warning);
            }
        }

        private void ReloadCommandFile()
        {

        }

        public List<Group> ConvertFromCommandCacheToGroups()
        {
            throw new NotImplementedException();
        }

        public List<Group> TreeData
        {
            get
            {
                return _treeViewContentRepresentation;
            }
            set
            {
                _treeViewContentRepresentation = value;
                OnPropertyChanged("TreeData");
            }
        }
    }
}
