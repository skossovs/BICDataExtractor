﻿using BIC.Foundation.Interfaces;
using BIC.Scrappers.FinvizScrapper;
using BIC.Scrappers.FinvizScrapper.DataObjects;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQExtractorCommander.Strategy
{
    public class SecMasterStrategy : IStrategy
    {
        private StrategyParameters _strategyParameters;
        private IBridgeComponents _finvizComponent;
        private IStoppableStatusable<ILog> _stoppableStatusable;
        public SecMasterStrategy(StrategyParameters strategyParameters, IStoppableStatusable<ILog> stoppableStatusable)
        {
            _strategyParameters = strategyParameters;
            _stoppableStatusable = stoppableStatusable;
        }
        public void Execute()
        {
            _finvizComponent = new FinvizBridgeComponents(_strategyParameters.Sector, _stoppableStatusable);
            _finvizComponent.Scrap<OverviewData>();
        }
    }
}
