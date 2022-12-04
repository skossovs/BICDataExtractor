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
        private string _url;
        public void Initiate(string url)
        {
            var client = (HttpClient)this._objRef;

            _url = url;
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string RunGetRequest()
        {
            var client = (HttpClient)this._objRef;

            var task = Task<HttpResponseMessage>.Run(async () => await client.GetAsync(_url));
            task.Wait(2000);
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
