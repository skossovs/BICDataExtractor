using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.Utils.Logger;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using BIC.Scrappers.YahooScrapper;
using System.Text;
using System.Threading.Tasks;


namespace BIC.YahooScrapper.Chain
{
    public class JsonObjectExtractor<T> : IActor where T : YahooFinanceData, new()
    {
        public string json_path;
        private ILog _logger = LogServiceProvider.Logger;

        public IActor Next { get; set; }

        public JsonObjectExtractor(string jsonPath)
        {
            json_path = jsonPath;
        }

        public bool Do(Context ctx)
        {
            var ticker = ctx.Parameters.Ticker;

            foreach (var jsonString in ctx.JsonContentLines)
            {
                var jsonObject = JObject.Parse(jsonString);

                var jToken = jsonObject.SelectToken(json_path);
                if (jToken.Count() == 0)
                    continue;

                var lst = new List<T>();

                foreach (var jt in jToken.First.Values())
                {
                    var v = jt.ToObject<T>();
                    v.Ticker = ticker;
                    lst.Add(v);
                }
                var allPageData = lst.AsEnumerable<T>();

                // Save List of types into file system as json file
                var fileName = BIC.Scrappers.Utils.FileHelper.ComposeFileName(typeof(T), true, "json");
                var fullPath = System.IO.Path.Combine(Settings.GetInstance().OutputDirectory, fileName);
                _logger.Info("Saving data into json file: " + fullPath);

                Exception ex;
                var result = BIC.Scrappers.Utils.FileHelper.SaveAsJSON(allPageData, fullPath, out ex);
                if (result == false)
                {
                    _logger.Warning("Failed to extract table data");
                    _logger.ReportException(ex);
                }

                return result;
            }

            return false;
        }
    }
}
