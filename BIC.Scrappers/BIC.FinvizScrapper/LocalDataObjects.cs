using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class PageMetric
    {
        public int RecordsPerPage {
            get
            {
                return 20; // fixed for Finviz
            }
        }
        public int NumberOfPages { get; set; }
    }

    public class FinancialViewRecord
    {
        public int No { get; set; }
        public string Ticker { get; set; }
        public double MarketCap { get; set; }
        public double Dividend { get; set; }
        public double ROA { get; set; }
        public double ROE { get; set; }
        public double ROI { get; set; }
        public double CurrRatio { get; set; }
        public double QuickRatio { get; set; }
        public double LTDebtToEquity { get; set; }
        public double DebtToEquity { get; set; }
        public double GrossMargin { get; set; }
        public double OperatingMargin { get; set; }
        public double ProfitMargin  { get; set; }
        public DateTime Earnings { get; set; }
        public double Price { get; set; }
        public double Change { get; set; }
        public long Volume { get; set; }
    }
}
