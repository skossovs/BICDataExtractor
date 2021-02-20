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

namespace BIC.Scrappers.Utils
{
    public class RequestHelper
    {
        // https://sites.google.com/a/chromium.org/chromedriver/
        public static string GetData(string url)
        {
            if (!CheckChrome())
                throw new Exception(string.Format("Chrome application is not found at referenced path: {0}", Settings.GetInstance().ChromeLocation));

            var programmerLinks = new List<string>();

            var options = new ChromeOptions()
            {
                BinaryLocation = Settings.GetInstance().ChromeLocation
            };

            options.AddArguments(new List<string>() { "headless", "disable-gpu" });
            var browser = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            browser.Navigate().GoToUrl(url);
            return browser.PageSource;
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
