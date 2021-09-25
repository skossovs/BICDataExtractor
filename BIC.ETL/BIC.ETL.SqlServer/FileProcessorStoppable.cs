using BIC.Foundation.Interfaces;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    // TODO: Refactor it, everything can be palced in base class.
    public class FileProcessorStoppable : FileProcessor
    {
        private IStoppableStatusable<ILog> _comm;

        public FileProcessorStoppable(IStoppableStatusable<ILog> comm)
        {
            _comm = comm;
            // interfere into logging process in order to send statuses to UI
            _logger = _comm.OverrideLogger(this._logger);
        }
    }
}
