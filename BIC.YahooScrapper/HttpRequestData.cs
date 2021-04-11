using BIC.Scrappers.Utils.Attributes;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Scrappers.YahooScrapper
{
    public class HttpRequestData
    {
        // The Scetch
        // https://finance.yahoo.com/quote/V/balance-sheet?p=V

        private ILog _logger = LogServiceProvider.Logger;
        public readonly string url = Settings.GetInstance().UrlRoot;
        public string[] Separators = { "?//" }; // should be populated in reverse. Attention number of characters in each group should match exactly with Maximum Order

            [AddressAttribute(Order = 0, Group = 0, Template = "quote/{0}")]
            public string Ticker1
            {
                get { return Ticker; }
            }
            [AddressAttribute(Order = 1, Group = 0, Template = "{0}")]
            public string ReportType { get; set; }
            [AddressAttribute(Order = 2, Group = 0, Template = "p={0}")]
            public string Ticker { get; set; }


        public string GenerateAddressRequest()
        {
            var sbAddress = new StringBuilder(url);
            var stacks = Separators.Select(s => new Stack<char>(s.ToCharArray())).ToArray();
            return ProcessAttributes(this, sbAddress, stacks);
        }

        private string ProcessAttributes(Object o, StringBuilder sbAdressPart, Stack<char>[] stacks)
        {
            var t = o.GetType();
            // Loop through all properties
            foreach (var p in t
                               .GetProperties()
                               .OrderBy(p => ((AddressAttribute)p.GetCustomAttributes(false)[0]).Order)
                               .OrderBy(p => ((AddressAttribute)p.GetCustomAttributes(false)[0]).Group))
            {
                var a = (AddressAttribute)p.GetCustomAttributes(false)[0];
                //var t = p.GetType();
                var oValue = p.GetValue(o);
                if (oValue == null) continue;

                var separator = Convert.ToString(stacks[a.Group].Pop());
                if (p.PropertyType == typeof(System.String))
                {
                    var segment = string.Format(a.Template, oValue);
                    sbAdressPart.Append(separator + segment);
                }
                else
                {
                    StringBuilder sbAdressPart1 = new StringBuilder();
                    var sValue = ProcessAttributes(oValue, sbAdressPart1, stacks);
                    var segment = string.Format(a.Template, sValue);
                    sbAdressPart.Append(separator + segment);
                }
            }
            _logger.Debug(@"Processing address part: ""{0}""", sbAdressPart.ToString());
            return sbAdressPart.ToString();
        }

        private string GenerateSeparator(Stack<char>[] stacks, int group, bool isNull)
        {
            if (isNull)
                return null;
            else
                return Convert.ToString(stacks[group].Pop());
        }
    }
}
