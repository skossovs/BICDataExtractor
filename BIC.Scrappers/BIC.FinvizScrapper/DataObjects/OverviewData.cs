using BIC.Scrappers.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper.DataObjects
{
    public class OverviewData
    {
        [PropertyMappingAttribute(ColumnNameOnTheSite = "No.")]
        public int? No { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Ticker")]
        public string Ticker { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Company")]
        public string FullName { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Sector")]
        public string Sector { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Industry")]
        public string Industry { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Country")]
        public string Country { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Market Cap")]
        public Decimal? MarketCap { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "P/E")]
        public Decimal? PE { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Price")]
        public Decimal? Price { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Change")]
        public Decimal? PriceChange { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Volume")]
        public Decimal? Volume { get; set; }
    }
}
