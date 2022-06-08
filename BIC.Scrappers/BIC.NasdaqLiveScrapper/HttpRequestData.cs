using BIC.Scrappers.Utils.Attributes;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.NasdaqLiveScrapper
{
    public class HttpRequestData
    {
        // https://api.nasdaq.com/api/quote/AAPL/realtime-trades?&limit=20&offset=20&fromTime=00:00
        // https://api.nasdaq.com/api/quote/AAPL/realtime-trades?&limit=100 - offset & fromTime are not working


        private ILog _logger = LogServiceProvider.Logger;
        public readonly string url = Settings.GetInstance().UrlRoot;
        public string[] Separators = { "/" }; // should be populated in reverse. Attention number of characters in each group should match exactly with Maximum Order

        [AddressAttribute(Order = 0, Group = 0, Template = "api/quote/{0}/realtime-trades?&limit=100")]
        public string Ticker { get; set; }

        public string GenerateAddressRequest()
        {
            var sbAddress = new StringBuilder(url);
            var stacks = Separators.Select(s => new Stack<char>(s.ToCharArray())).ToArray();
            return ProcessAttributes(this, sbAddress, stacks);
        }
        // TODO: get rid of copy-paste
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













        // RESPONCE
        /*

        {
           "data":{
              "symbol":"aapl",
              "totalRecords":100,
              "offset":0,
              "limit":100,
              "headers":{
                 "nlsTime":"NLS Time (ET)",
                 "nlsPrice":"NLS Price",
                 "nlsShareVolume":"NLS Share Volume"
              },
              "rows":[
                 {
                    "nlsTime":"10:39:00",      ==>> take time from here
                    "nlsPrice":"$ 148.3299",
                    "nlsShareVolume":"300"
                 },

                 {
                    "nlsTime":"10:38:29",
                    "nlsPrice":"$ 148.22",
                    "nlsShareVolume":"222"
                 },
                 {
                    "nlsTime":"10:38:29",
                    "nlsPrice":"$ 148.2",
                    "nlsShareVolume":"100"
                 }
              ],
              "topTable":{
                 "headers":{
                    "nlsVolume":"Nasdaq Last Sale (NLS) Plus Volume",
                    "previousClose":"Previous Close",
                    "todayHighLow":"Today's High / Low*",
                    "fiftyTwoWeekHighLow":"52 Week High / Low"
                 },
                 "rows":[
                    {
                       "nlsVolume":"18,428,953",
                       "previousClose":"$145.38",
                       "todayHighLow":"$148.4773/$147.03",
                       "fiftyTwoWeekHighLow":"$182.94/$123.85"
                    }
                 ]
              },
              "description":{
                 "message":"*Today’s High/Low is only updated during regular trading hours; and does not include trades occurring in pre-market or after-hours.",
                 "url":null
              },
              "message":[
                 "This page refreshes every 30 seconds.",
                 "Data last updated Jun 06, 2022 10:39 AM ET"     ==>> take date from here
              ]
            },
            "message":null,
            "status":{
               "rCode":200,
               "bCodeMessage":null,
               "developerMessage":null
            }
        }
         */
    }
}
