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
