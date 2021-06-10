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
    /// 1. Start/Stop windows process. Done
    /// 2. Accept process parameters
    /// 3. Provide result of process ending
    /// 4. Transfer statuses from running process. DONE
    /// </summary>
    public class ControlProcessViewModel : INotifyPropertyChanged
    {
        private DelegateCommand<string> _startCommand;
        private DelegateCommand<string> _stopCommand;
        private string                  _processType;
        private EProcessStatus          _status;

        private IList<TreeNode>         _treeNodes;
        private TreeNode                _selectedTreeNode;


        public ControlProcessViewModel()
        {
            _startCommand = new DelegateCommand<string>(
                (arguments) =>
                {
                    var e = (EProcessType)Enum.Parse(typeof(EProcessType), this.ProcessType);
                    if(e == EProcessType.Scrapper) // TODO: too many ifs here, tree has process settings
                    {
                        var path = _selectedTreeNode.Properties["path"]; // TODO: magic name
                        var cmdArgs = PathIntoArguments(path);
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStartMessage(cmdArgs.Item2, e));
                    }
                    else if(e == EProcessType.ETL)
                    {
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStartMessage(string.Empty, e));
                    }
                });
            _stopCommand = new DelegateCommand<string>(
                (_) =>
                {
                    var e = (EProcessType)Enum.Parse(typeof(EProcessType), this.ProcessType);
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new ProcessStopMessage(e));
                });

            // tree commands
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ProcessStatusMessage>(this, ReceiveStatus);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<TreeViewPopulatedMessage>(this, ReceiveTreeViewPopulatedCommand);
        }

        private Tuple<string, string> PathIntoArguments(string path)
        {
            var cmd = string.Empty;
            var args = string.Empty;

            var members = path.Split(new char[] { (char)0x00 });
            if(members.Count() > 2)
            {
                cmd = members[1];
                args = String.Join(" ", members.Skip(2));
            }
            else if(members.Count() == 2)
            {
                cmd = members[1];
            }

            var result = new Tuple<string, string>(cmd, args);

            return result;
        }

        private void ReceiveStatus(ProcessStatusMessage s)
        {
            if(s.ProcessType == (EProcessType)Enum.Parse(typeof(EProcessType), _processType))
                Status = s.ProcessStatus;
        }

        public string ProcessType
        {
            get
            {   return _processType;   }
            set
            {
                _processType = value;
                // don't process tree messages if it is etl process
                if(_processType == "Scrapper")
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<TreeViewSelectionMessage>(this, ReceiveTreeViewSelectionCommand);
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


        public IList<TreeNode> TreeNodes
        {
            get
            {
                return _treeNodes;
            }
            set
            {
                _treeNodes = value;
                OnPropertyChanged("TreeNodes");
            }
        }

        public TreeNode SelectedNode
        {
            get
            {
                return _selectedTreeNode;
            }
            set
            {
                _selectedTreeNode = value;
                OnPropertyChanged("SelectedNode");
            }
        }

        public int? SelectedNodeKey
        {
            get { return _selectedTreeNode?.Key; }
            set
            {
                OnPropertyChanged("SelectedNodeKey");
            }
        }

        private void ReceiveTreeViewPopulatedCommand(TreeViewPopulatedMessage tvPopulatedMessage)
        {
            TreeNodes = tvPopulatedMessage.TreeNodes;
        }
        private void ReceiveTreeViewSelectionCommand(TreeViewSelectionMessage tvSelectedMessage)
        {
            SelectedNode = TreeNodes.First(t => t.Key == tvSelectedMessage.Key);
            SelectedNodeKey = tvSelectedMessage.Key;
        }

        public EProcessStatus Status
        {
            get
            {    return _status;    }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
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
