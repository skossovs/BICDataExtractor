using BIC.WPF.ScrapManager.Data;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.MVVM.Messages
{
    class TreeViewPopulatedMessage : MessageBase
    {
        public IList<TreeNode> TreeNodes { get; set; }
        public TreeViewPopulatedMessage(IList<TreeNode> treeNodes)
        {
            TreeNodes = treeNodes;
        }
    }
}
