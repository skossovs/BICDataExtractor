using BIC.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.ETL.SqlServer
{
    [AppSettingsXML]
    class Settings
    {
        [Mandatory]
        public string InputDirectory { get; set; }
    }
}
