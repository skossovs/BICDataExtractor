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
        private readonly static object       delayLocker = new object();
        public  readonly static int          TimeDelay   = Settings.GetInstance().TimeDelayInSeconds * 1000;
        private readonly static ILog         _logger     = LogServiceProvider.Logger;
        private readonly static ChromeDriver _browser;

        static RequestHelper()
        {
            if (!CheckChrome())
                throw new Exception(string.Format("Chrome application is not found at referenced path: {0}", Settings.GetInstance().ChromeLocation));

            // Single chrome object is needed, otherwise Chrome.exe will be created at each call
            var options = new ChromeOptions()
            {
                BinaryLocation = Settings.GetInstance().ChromeLocation
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });
            _browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
        }

        // This one is exotic way to call static destructor
        #region static destructor
        private sealed class Destructor
        {
            ~Destructor()
            {
                _browser.Dispose();
            }
        }
        private static readonly Destructor Finalise = new Destructor();
        #endregion

        // https://sites.google.com/a/chromium.org/chromedriver/
        public static string GetData(string url)
        {
            lock (delayLocker)
            {
                _logger.Debug(url);

                var dom = url
                    .TryCatch((u) =>
                    {
                        var programmerLinks = new List<string>();
                        _browser.Navigate().GoToUrl(url);
                        Thread.Sleep(TimeDelay);
                        return _browser.PageSource;
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
