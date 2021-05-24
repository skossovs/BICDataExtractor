using BIC.Scrappers.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.MoneyConverterScrapper.DataObjects
{
    public class FxUsdData
    {
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Picture")]
        public string  Picture { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Currency")]
        public string  Currency { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Rate")]
        public decimal Rate     { get; set; }
    }
}
