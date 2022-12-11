using BIC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class HttpClientApartmentProxy : ApartmentProxy<HttpClient>
    {
        private const int YAHOO_CONFIGURATION          = 0;
        private const int FINVIZ_CONFIGURATION         = 1; // unfortuantely I am not able to transfer Func<> object to Initiate method, since we are operating in different app domain
        private const int MONEYCONVERTER_CONFIGURATION = 2;
        private string _url;
        public void Initiate(string url, int configuration)
        {
            var client = (HttpClient)this._objRef;

            _url = url;
            client.BaseAddress = new Uri(url);
            if (configuration == YAHOO_CONFIGURATION)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            else if (configuration == FINVIZ_CONFIGURATION)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
            }
            else if (configuration == MONEYCONVERTER_CONFIGURATION)
            {
                // TODO: FX failing for some reason, don't know what headers need to be provided
            }
        }

        public string RunGetRequest()
        {
            var client = (HttpClient)this._objRef;

            var task = Task<HttpResponseMessage>.Run(async () => await client.GetAsync(_url));
            task.Wait(2000); // TODO: ?? not needed
            client.CancelPendingRequests();

            if (task.Status == TaskStatus.Canceled)
                return "ERROR";

            HttpResponseMessage response = task.Result;
            if (response.IsSuccessStatusCode)
            {
                var task1 = Task<string>.Run(async () => await response.Content.ReadAsStringAsync());
                var data = task1.Result;

                return data;
            }

            return "ERROR";
        }


        new public static HttpClientApartmentProxy CreateInstance(string domainName)
        {
            var dom = AppDomain.CreateDomain(domainName);
            var instance = (HttpClientApartmentProxy)dom.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(HttpClientApartmentProxy).FullName);
            instance._dom = dom;

            return instance;
        }

        new public static void DestroyDomain(HttpClientApartmentProxy p)
        {
            AppDomain.Unload(p._dom);
        }
    }
}
