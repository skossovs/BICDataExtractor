using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.FinvizScrapper
{
    public class FinvizFilterComboboxes
    {
        public struct Item
        {
            public string Label;
            public string Value;
        }
        public List<Item> ExchangeFilter { get; set; }
        public List<Item> IndexFilter    { get; set; }
        public List<Item> SectorFilter   { get; set; }
        public List<Item> IndustryFilter { get; set; }
        public List<Item> CountryFilter  { get; set; }
    }
}
