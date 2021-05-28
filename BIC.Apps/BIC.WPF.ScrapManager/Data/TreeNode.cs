using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.WPF.ScrapManager.Data
{
    class TreeNode
    {
        public int Key { get; set; }
        public TreeNodeType Type { get; set; }
        public Dictionary<string, string> Properties { get; set; }

    }
}
