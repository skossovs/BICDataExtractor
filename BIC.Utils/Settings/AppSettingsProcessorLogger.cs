using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.SettingProcessors
{
    public class AppSettingsProcessorLogger : AppSettingsProcessor
    {
        private readonly static ILog _logger = Logger.LogServiceProvider.Logger;

        public override bool Populate()
        {
            bool result = base.Populate();

            foreach(var statusRecord in base.ListPropertyReadingStatuses)
            {
                if (statusRecord.IsSuccessful)
                    _logger.Debug(@"Successfully set parameter ""{0}.{1}"" to {2}", statusRecord.PropertyAssembly, statusRecord.PropertyName, statusRecord.PropertyValue);
                else
                    _logger.Error(@"Failed to initialize ""{0}.{1}"" with {2}", statusRecord.PropertyAssembly, statusRecord.PropertyName, statusRecord.ErrorMessage);
            }

            return result;
        }
    }
}
