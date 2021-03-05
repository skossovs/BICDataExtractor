using BIC.Scrappers.Utils.Attributes;
using System;

namespace BIC.Scrappers.FinvizScrapper.DataObjects
{
    public class FinancialData
    {
        [PropertyMappingAttribute(ColumnNameOnTheSite = "No.")]
        public int No { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Ticker")]
        public string Ticker { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Market Cap")]
        public Decimal MarketCap { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Dividend")]
        public Decimal Dividend { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "ROA")]
        public Decimal ROA { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "ROE")]
        public Decimal ROE { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "ROI")]
        public Decimal ROI { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Current R")]
        public Decimal CurrentRatio { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Quick R")]
        public Decimal QuickRatio { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "LTDebt/Eq")]
        public Decimal LongTermDebtToEquity { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Debt/Eq")]
        public Decimal DebtToEquity { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Gross M")]
        public Decimal GrossMargin { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Oper M")]
        public Decimal OperationMargin { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Profit M")]
        public Decimal ProfitMargin { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Earnings")]
        public Decimal Earnings { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Price")]
        public Decimal Price { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Change")]
        public Decimal Change { get; set; }
        [PropertyMappingAttribute(ColumnNameOnTheSite = "Volume")]
        public Decimal Volume { get; set; }
    }
}
