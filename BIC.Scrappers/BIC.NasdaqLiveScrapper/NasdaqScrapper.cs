using BIC.Foundation.Interfaces;
using BIC.NasdaqLiveScrapper.DataObjects;
using BIC.Scrappers.Utils;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Simple);
            var request = new HttpRequestData() { Ticker = ticker };
            var url = request.GenerateAddressRequest();
            var currentPagehtmlContent = retriever.GetData(url);

            var path = System.IO.Path.Combine(OutputDirectory, "Nasdaq.json");
            System.IO.File.WriteAllText(path, currentPagehtmlContent, Encoding.ASCII);

            //var cqHelper = new CQHelper();
            //var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            return result;
        }


    }
}
