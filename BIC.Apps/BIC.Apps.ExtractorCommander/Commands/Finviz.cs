using BIC.Scrappers.FinvizScrapper;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.ExtractorCommander.Commands
{
    public static class Finviz
    {
        private static ILog                   _logger = LogServiceProvider.Logger;
        private static FinvizFilterComboboxes _filters;

        static Finviz()
        {
            using (StreamReader rd = new StreamReader("FinvizFilterComboboxes.yaml")) // TODO: hardcoded file location, misplaced initialization responsibility
            {
                _filters = Utils.YamlOperations<FinvizFilterComboboxes>.ReadObjectFromStream(rd);
            }
        }

        public static void ScrapOverview()
        {
            Scrap<OverviewData>();
        }

        public static void ScrapOverview(string sector)
        {
            Scrap<OverviewData>(sector);
        }

        public static void ScrapFinancial()
        {
            Scrap<FinancialData>();
        }

        public static void ScrapFinancial(string sector)
        {
            Scrap<FinancialData>(sector);
        }

        private static void Scrap<T>() where T : class, new()
        {
            foreach (var sectorItem in _filters.SectorFilter)
            {
                Scrap<T>(sectorItem.Value);
            }
        }
        private static void Scrap<T>(string sector) where T: class, new()
        {
            // Find sector by value
            var sectorItem = _filters.SectorFilter.Find(s => s.Value == sector);
            if (sectorItem == null)
                throw new Exception($"Requested sector cannot be found by value {sector}");

            _logger.Info($"Loading Sector {sectorItem.Label} ..");
            var allPageScrapper = new AllPageScrapper<T>();
            var fp = new FinvizParameters()
            {
                View = EView.Overview,
                FilterView = EFilterView.All,
                Filters = new Filters()
                {
                    SectorFilter = sector,
                }
            };

            allPageScrapper.Scrap(fp);
            _logger.Info($"Finished Loading Sector {sectorItem.Label}.");
        }
    }
}
