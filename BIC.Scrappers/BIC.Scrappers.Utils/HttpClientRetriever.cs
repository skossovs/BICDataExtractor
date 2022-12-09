using BIC.Foundation.Interfaces;
using BIC.Utils.Logger;
using BIC.Utils.Monads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class HttpClientRetriever : IContentRetriever
    {
        private readonly object    _delayLocker = new object();
        private readonly int       _configuration;
        private readonly ILog      _logger = LogServiceProvider.Logger;
        protected Delayers.Delayer _delayer;

        public HttpClientRetriever(Delayers.Delayer delayer, int configuration)
        {
            _delayer       = delayer;
            _configuration = configuration;
        }
        public string GetData(string url)
        {
            lock (_delayLocker)
            {
                _logger.Debug(url);
                string dom = url
                .TryCatch((u) =>
                {
                    _delayer.Wait();
                    var proxy = HttpClientApartmentProxy.CreateInstance("HttpClientApartmentProxy");
                    proxy.Initiate(u, _configuration);
                    string result = proxy.RunGetRequest();
                    HttpClientApartmentProxy.DestroyDomain(proxy);
                    return result;
                }, _logger.ReportException);
                return dom.Do(_ => _logger.Info("Recieved data."));
            }
        }
    }
}
