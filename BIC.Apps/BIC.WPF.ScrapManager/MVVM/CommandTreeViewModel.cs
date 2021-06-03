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
            // TODO: Implement
        }

        public List<Group> ConvertFromCommandCacheToGroups()
        {
            List<TreeNode> lstNodes = new List<TreeNode>();

            var RootGroup = new Group()
            {
                Key       = _content.CommandLines.GetHashCode(),
                Name      = "Root",
                Path      = "Root",
                SubGroups = new List<Group>(),
                Entries   = new List<Entry>()
            };

            var properties = new Dictionary<string, string>();
            properties.Add("Root", "Root");

            lstNodes.Add(new TreeNode()
            {
                Type       = TreeNodeType.Root,
                Key        = ("Root").GetHashCode(),
                Properties = properties
            });

            var RootGroups = new List<Group>() { RootGroup };
            var parentPath = RootGroup.Path;

            _content?.CommandLines?.ForEach(c =>
            {
                Group g = new Group();
                g.Name  = c.CommandLine;
                g.Key   = c.CommandLine.GetHashCode();
                g.Path  = parentPath + (char)0x00 + c.CommandLine;

                c.CommandParameters?.ForEach(p =>
                {
                    FillParametersRecursive(g, p, parentPath + (char)0x00 + c.CommandLine);
                });

                RootGroup.SubGroups.Add(g);

                // TODO: DROP IT!!
                //properties = new Dictionary<string, string>();
                //properties.Add(ExcelContentConstants.ExcelFilePathNodeName, f.ExcelFileFullPath);

                //lstNodes.Add(new TreeNode()
                //{
                //    Type = TreeNodeType.ExcelFile,
                //    Key = g.Key,
                //    Properties = properties
                //});

                //f?.Sheets?.ForEach(s =>
                //{
                //    g.Entries.Add(new Entry()
                //    {
                //        Name = s.SheetName,
                //        Path = g.Path + (char)0x00 + s.SheetName,
                //        Key = (g.Path + (char)0x00 + s.SheetName).GetHashCode()
                //    });

                //    properties = new Dictionary<string, string>();
                //    properties.Add(ExcelContentConstants.ExcelFilePathNodeName, f.ExcelFileFullPath);
                //    properties.Add(ExcelContentConstants.SheetNameNode, s.SheetName);
                //    properties.Add(ExcelContentConstants.ClassNameNode, s.ClassName);

                //    lstNodes.Add(new TreeNode()
                //    {
                //        Type = TreeNodeType.ExcelSheet,
                //        Key = (g.Path + (char)0x00 + s.SheetName).GetHashCode(),
                //        Properties = properties
                //    });
                //});
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new TreeViewPopulatedMessage(lstNodes));
            return RootGroups;
        }

        private void FillParametersRecursive(Group gCurrent, CommandParameter currentParameter, string parentPath)
        {
            Group g = new Group();
            g.Name = currentParameter.ParameterLine;
            string curentPath = gCurrent.Path + (char)0x00 + currentParameter.ParameterLine;
            g.Key = curentPath.GetHashCode();
            g.Path = curentPath;

            currentParameter.DrillDownParameters.ForEach(p =>
            {
                FillParametersRecursive(g, p, curentPath);
            });

            gCurrent.SubGroups.Add(g);
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
