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
        private string _url = Settings.GetInstance().UrlRoot;

        public bool Scrap()
        {
            bool result = false;

            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var currentPagehtmlContent = retriever.GetData(_url);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);



            return result;
        }


    }
}
