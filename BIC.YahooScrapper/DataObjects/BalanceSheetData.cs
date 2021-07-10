using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper.DataObjects
{
    public class BalanceSheetData : YahooFinanceData
    {
        public YahooFinanceNumber intangibleAssets        { get; set; }
        public YahooFinanceNumber totalLiab               { get; set; }
        public YahooFinanceNumber totalStockholderEquity  { get; set; }
        public YahooFinanceNumber otherCurrentLiab        { get; set; }
        public YahooFinanceNumber totalAssets             { get; set; }
        public YahooFinanceNumber endDate                 { get; set; }
        public YahooFinanceNumber commonStock             { get; set; }
        public YahooFinanceNumber otherCurrentAssets      { get; set; }
        public YahooFinanceNumber retainedEarnings        { get; set; }
        public YahooFinanceNumber otherLiab               { get; set; }
        public YahooFinanceNumber goodWill                { get; set; }
        public YahooFinanceNumber treasuryStock           { get; set; }
        public YahooFinanceNumber otherAssets             { get; set; }
        public YahooFinanceNumber cash                    { get; set; }
        public YahooFinanceNumber totalCurrentLiabilities { get; set; }
        public YahooFinanceNumber shortLongTermDebt       { get; set; }
        public YahooFinanceNumber otherStockholderEquity  { get; set; }
        public YahooFinanceNumber propertyPlantEquipment  { get; set; }
        public YahooFinanceNumber totalCurrentAssets      { get; set; }
        public YahooFinanceNumber longTermInvestments     { get; set; }
        public YahooFinanceNumber netTangibleAssets       { get; set; }
        public YahooFinanceNumber shortTermInvestments    { get; set; }
        public YahooFinanceNumber netReceivables          { get; set; }
        public int           maxAge                  { get; set; }
        public YahooFinanceNumber longTermDebt            { get; set; }
        public YahooFinanceNumber inventory               { get; set; }
        public YahooFinanceNumber accountsPayable         { get; set; }
    }

    public class BalanceSheetDataQuarterly : BalanceSheetData
    {
    }
}
