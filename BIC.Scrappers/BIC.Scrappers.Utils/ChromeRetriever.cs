using BIC.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BIC.Utils.Logger;
using BIC.Utils.Monads;

namespace BIC.Scrappers.Utils
{
    public class ChromeRetriever : IContentRetriever
    {
        private readonly object       delayLocker = new object();
        public  readonly int          TimeDelay   = Settings.GetInstance().TimeDelayInSeconds * 1000;
        private readonly ILog         _logger     = LogServiceProvider.Logger;
        private readonly ChromeDriver _browser;
        protected Delayers.Delayer    _delayer;

        public ChromeRetriever(Delayers.Delayer delayer)
        {
            if (!CheckChrome())
                throw new Exception(string.Format("Chrome application is not found at referenced path: {0}", Settings.GetInstance().ChromeLocation));

            _delayer = delayer;
            // Single chrome object is needed, otherwise Chrome.exe will be created at each call
            var options = new ChromeOptions()
            {
                BinaryLocation = Settings.GetInstance().ChromeLocation
            };
            options.AddArguments(new List<string>() { "headless", "disable-gpu", "silent" });
            /// Cloud Flare problem to resolve
            /// https://www.api2pdf.com/solved-access-denied-v2018-api2pdf-com-used-cloudflare-to-restrict-access/
            options.AddArgument("--user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
            _browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
        }

        ~ChromeRetriever()
        {
            _browser.Dispose();
        }

        private bool? _chromeCheckOnce;
        private bool CheckChrome()
        {
            if (!_chromeCheckOnce.HasValue)
                _chromeCheckOnce = File.Exists(Settings.GetInstance().ChromeLocation);
            return _chromeCheckOnce.Value;
        }

        // https://sites.google.com/a/chromium.org/chromedriver/
        // https://stackoverflow.com/questions/17998162/how-to-change-user-agent-in-selenium-with-net
        public string GetData(string url)
        {
            lock (delayLocker)
            {
                _logger.Debug(url);

                var dom = url
                    .TryCatch((u) =>
                    {
                        var programmerLinks = new List<string>();
                        _browser.Navigate().GoToUrl(url);
                        _delayer.Wait();
                        return _browser.PageSource;
                    }, _logger.ReportException);

                return dom.Do(_ => _logger.Info("Recieved data."));
            }
        }
    }
}
