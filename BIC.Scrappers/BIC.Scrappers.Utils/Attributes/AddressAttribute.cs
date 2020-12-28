using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AddressAttribute : Attribute
    {
        public int Order { get; set; }
        public int Group { get; set; }
        public string Template { get; set; }
    }
}
