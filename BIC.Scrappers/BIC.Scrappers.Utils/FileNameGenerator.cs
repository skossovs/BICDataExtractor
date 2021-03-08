using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public static class FileHelper
    {
        public static string ComposeFileName(Type t, bool includeTimestamp, string extension)
        {
            string timeStamp = "";

            if (includeTimestamp)
                timeStamp = "_" + DateTime.Now.ToLongTimeString().Replace(':', '-');
            return t.Assembly.GetName().Name + "_" + t.Name + timeStamp + "." + extension;
        }

        public static bool SaveAsJSON<T>(IEnumerable<T> data, string fullFileName, out Exception ex)
        {
            ex = null;

            try
            {
                string contents = JsonConvert.SerializeObject(data, Formatting.Indented);
                System.IO.File.WriteAllText(fullFileName, contents);
            }
            catch (Exception ex1)
            {
                ex = ex1;
                return false;
            }

            return true;
        }
    }
}
