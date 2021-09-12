using BIC.Scrappers.FinvizScrapper;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander
{
    public class FinvizBridgeComponents : IBridgeComponents
    {
        private ILog _logger = LogServiceProvider.Logger;
        private static FinvizFilterComboboxes _filters;
        private IStoppableStatusable _stoppableStatusable;
        private string _sector;

        static FinvizBridgeComponents()
        {
            using (StreamReader rd = new StreamReader("FinvizFilterComboboxes.yaml")) // TODO: hardcoded file location, misplaced initialization responsibility
            {
                _filters = Utils.YamlOperations<FinvizFilterComboboxes>.ReadObjectFromStream(rd);
            }
        }

        public FinvizBridgeComponents(string sector, IStoppableStatusable stoppableStatusable)
        {
            _sector = sector;
            _stoppableStatusable = stoppableStatusable;
            _logger = stoppableStatusable.OverrideLogger(_logger);
        }

        private class SectorData
        {
            public string Sector;
            public string SectorLabel;
        }

        private IEnumerable<SectorData> GenerateSectorDataFilter()
        {
            if (_sector != null)
            {
                var sectorItem = _filters.SectorFilter.Find(s => s.Value == _sector);
                if (sectorItem == null)
                    throw new Exception($"Requested sector cannot be found by value {_sector}");

                yield return new SectorData() { Sector = _sector, SectorLabel = sectorItem.Label };
            }
            else
            {
                foreach (var sectorItem in _filters.SectorFilter)
                {
                    yield return new SectorData() { Sector = sectorItem.Value, SectorLabel = sectorItem.Label };
                }
            }
        }

        public void Scrap<T>() where T : class, new()
        {
            foreach(var sectorItem in GenerateSectorDataFilter())
            {
                if (_stoppableStatusable.IsStopped)
                {
                    _logger.Info("#Stopped");
                    break;
                }

                _logger.Info($"Loading Sector {sectorItem.SectorLabel} ..");

                var allPageScrapper = new AllPageScrapperStoppable<T>(_stoppableStatusable);
                var fp = new FinvizParameters()
                {
                    FilterView = EFilterView.All,
                    Filters = new Filters()
                    {
                        SectorFilter = _sector,
                    }
                };

                var typeName = typeof(T).Name;
                switch (typeName)
                {
                    case "OverviewData":
                        fp.View = EView.Overview;
                        break;
                    case "FinancialData":
                        fp.View = EView.Financial;
                        break;
                    default:
                        _logger.Error($"Unsupported type {typeName}");
                        throw new Exception($"Unsupported type {typeName}");
                }

                allPageScrapper.Scrap(fp);
                _logger.Info($"Finished Loading Sector {sectorItem.SectorLabel}.");
            }
            _logger.Info("#Finished");
        }
    }
}
