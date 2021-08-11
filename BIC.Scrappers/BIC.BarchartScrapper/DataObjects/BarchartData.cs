using BIC.Scrappers.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.BarchartScrapper.DataObjects
{
    public class BarchartData
    {
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Symbol")]
        public string Ticker { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Name")]
        public string Name { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Std Dev")]
        public decimal StandardDeviation { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Last")]
        public decimal Last { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Change")]
        public decimal Change { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "%Chg")]
        public decimal ChangePercentage { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "High")]
        public decimal High { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Low")]
        public decimal Low { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Volume")]
        public decimal Volume { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Time")]
        public DateTime Date { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Links")]
        public string Links { get; set; }
    }
}
