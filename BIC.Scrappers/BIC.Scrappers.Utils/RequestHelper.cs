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
        //public static string GetData(string url)
        //{
        //    // WebClient is still convenient
        //    // Assume UTF8, but detect BOM - could also honor response charset I suppose
        //    using (var client = new WebClient())
        //    using (var stream = client.OpenRead(url))
        //    using (var textReader = new StreamReader(stream, Encoding.UTF8, true))
        //    {
        //        return textReader.ReadToEnd();
        //    }
        //}


        public static string GetData(string url)
        {
            //string fullUrl = "https://en.wikipedia.org/wiki/List_of_programmers";
            List<string> programmerLinks = new List<string>();

            var options = new ChromeOptions()
            {
                BinaryLocation = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe"
            };
            // TODO: Remove hardcoded paths
            options.AddArguments(new List<string>() { "headless", "disable-gpu" });
            var browser = new ChromeDriver(@"C:\Users\Stan\Documents\GitHub\BICDataExtractor\packages\Selenium.Chrome.WebDriver.88.0.0", options);
            browser.Navigate().GoToUrl(url);
            return browser.PageSource;
        }
    }
}
