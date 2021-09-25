﻿using BIC.Foundation.Interfaces;
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
        private FinvizFilterComboboxes _filters;
        private IStoppableStatusable<ILog> _stoppableStatusable;
        private string _sector;

        public FinvizBridgeComponents(string sector, IStoppableStatusable<ILog> stoppableStatusable)
        {
            _sector = sector;
            _stoppableStatusable = stoppableStatusable;
            _logger = stoppableStatusable.OverrideLogger(_logger);

            try
            {
                string currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                using (StreamReader rd = new StreamReader(Path.Combine(currentPath, "FinvizFilterComboboxes.yaml"))) // TODO: hardcoded file location, misplaced initialization responsibility
                {
                    _filters = Utils.YamlOperations<FinvizFilterComboboxes>.ReadObjectFromStream(rd);
                }
            }
            catch (Exception ex)
            {
                _logger.ReportException(ex);
                throw;
            }

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
            _logger.Info("#Running");

            foreach (var sectorItem in GenerateSectorDataFilter())
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
