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

        public bool Scrap()
        {
            bool result = false;

            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var request = new HttpRequestData();
            var url = request.GenerateAddressRequest();
            var currentPagehtmlContent = retriever.GetData(url);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            return result;
        }


    }
}
