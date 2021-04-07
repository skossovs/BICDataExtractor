using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    class BalanceSheetData
    {
    }

    class BalanceSheetDataQuarterly : QuarterData
    {
        public QuarterNumber intangibleAssets        { get; set; }
        public QuarterNumber totalLiab               { get; set; }
        public QuarterNumber totalStockholderEquity  { get; set; }
        public QuarterNumber otherCurrentLiab        { get; set; }
        public QuarterNumber totalAssets             { get; set; }
        public QuarterNumber endDate                 { get; set; }
        public QuarterNumber commonStock             { get; set; }
        public QuarterNumber otherCurrentAssets      { get; set; }
        public QuarterNumber retainedEarnings        { get; set; }
        public QuarterNumber otherLiab               { get; set; }
        public QuarterNumber goodWill                { get; set; }
        public QuarterNumber treasuryStock           { get; set; }
        public QuarterNumber otherAssets             { get; set; }
        public QuarterNumber cash                    { get; set; }
        public QuarterNumber totalCurrentLiabilities { get; set; }
        public QuarterNumber shortLongTermDebt       { get; set; }
        public QuarterNumber otherStockholderEquity  { get; set; }
        public QuarterNumber propertyPlantEquipment  { get; set; }
        public QuarterNumber totalCurrentAssets      { get; set; }
        public QuarterNumber longTermInvestments     { get; set; }
        public QuarterNumber netTangibleAssets       { get; set; }
        public QuarterNumber shortTermInvestments    { get; set; }
        public QuarterNumber netReceivables          { get; set; }
        public int           maxAge                  { get; set; }
        public QuarterNumber longTermDebt            { get; set; }
        public QuarterNumber inventory               { get; set; }
        public QuarterNumber accountsPayable         { get; set; }
    }
}
