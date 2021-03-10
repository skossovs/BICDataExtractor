using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Utils.SettingProcessors
{
    public class AppSettingsProcessorLogger : AppSettingsProcessor
    {
        private readonly static ILog _logger = Logger.LogServiceProvider.Logger;

        public override bool Populate(Object settingsObject, Assembly a)
        {
            bool result = base.Populate(settingsObject, a);

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
