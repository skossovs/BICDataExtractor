using BIC.Foundation.Interfaces;
using BIC.Utils.Logger;
using BIC.Utils.Monads;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    // TODO: redo with simple request without google chrome app
    public class SimpleRetriever : IContentRetriever
    {
        private readonly object delayLocker = new object();
        public readonly int TimeDelay = Settings.GetInstance().TimeDelayInSeconds * 1000;
        private readonly ILog _logger = LogServiceProvider.Logger;
        private readonly ChromeDriver _browser;
        protected Delayers.Delayer _delayer;

        public SimpleRetriever(Delayers.Delayer delayer)
        {
            if (!CheckChrome())
                throw new Exception(string.Format("Chrome application is not found at referenced path: {0}", Settings.GetInstance().ChromeLocation));

            _delayer = delayer;
            var options = new ChromeOptions();
            //{
            //    BinaryLocation = Settings.GetInstance().ChromeLocation
            //};
            options.AddArguments(new List<string>() { "headless"});//, "disable-gpu", "silent" 
            // Single chrome object is needed, otherwise Chrome.exe will be created at each call
            _browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);//, options);
        }

        ~SimpleRetriever()
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
