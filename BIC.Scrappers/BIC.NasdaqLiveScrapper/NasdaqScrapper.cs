using BIC.Foundation.Interfaces;
using BIC.NasdaqLiveScrapper.DataObjects;
using BIC.Scrappers.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BIC.NasdaqLiveScrapper
{
    public class NasdaqScrapper<T> where T : NasdaqData
    {
        private ILog _logger = LogServiceProvider.Logger;
        public readonly string OutputDirectory = Settings.GetInstance().OutputDirectory;
        public bool Scrap(string ticker)
        {
            bool result = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // TODO: must be in constructor somewhere

            var request = new HttpRequestData() { Ticker = ticker };
            var url = request.GenerateAddressRequest();


            var currentPagehtmlContent = HttpGet(url);

            var path = System.IO.Path.Combine(OutputDirectory, "Nasdaq.json");
            System.IO.File.WriteAllText(path, currentPagehtmlContent, Encoding.ASCII);

            return result;
        }



        private string HttpGet(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


    }
}
