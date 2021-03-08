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
    public class RequestHelper
    {
        private readonly static object delayLocker = new object();
        public  readonly static int    TimeDelay   = Settings.GetInstance().TimeDelayInSeconds * 1000;
        private readonly static ILog   _logger     = LogServiceProvider.Logger;

        // https://sites.google.com/a/chromium.org/chromedriver/
        public static string GetData(string url)
        {
            if (!CheckChrome())
                throw new Exception(string.Format("Chrome application is not found at referenced path: {0}", Settings.GetInstance().ChromeLocation));

            lock (delayLocker)
            {
                _logger.Debug(url);

                var dom = url
                    .TryCatch((u) =>
                    {
                        var programmerLinks = new List<string>();

                        var options = new ChromeOptions()
                        {
                            BinaryLocation = Settings.GetInstance().ChromeLocation
                        };

                        options.AddArguments(new List<string>() { "headless", "disable-gpu" });
                        var browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
                        browser.Navigate().GoToUrl(url);

                        Thread.Sleep(TimeDelay);

                        return browser.PageSource;

                    }, _logger.ReportException);

                return dom.Do(_ => _logger.Info("Recieved data."));
            }
        }

        private static bool? _chromeCheckOnce;
        private static bool CheckChrome()
        {
            if(!_chromeCheckOnce.HasValue)
                _chromeCheckOnce = File.Exists(Settings.GetInstance().ChromeLocation);
            return _chromeCheckOnce.Value;
        }
    }
}
