using System;

namespace BIC.Scrappers.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMappingAttribute : System.Attribute
    {
        public string ColumnNameOnTheSite { get; set; }
    }
}
