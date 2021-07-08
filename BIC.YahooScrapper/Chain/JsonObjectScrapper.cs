using System;
using BIC.Foundation.Interfaces;
using BIC.Scrappers.Utils;
using BIC.Scrappers.YahooScrapper.DataObjects;
using BIC.Utils.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIC.Scrappers.YahooScrapper;

namespace BIC.YahooScrapper.Chain
{
    public class JsonObjectScrapper : IActor
    {
        public IActor Next { get; set; }

        public bool Do(Context ctx)
        {
            var r = new HttpRequestData()
            {
                Ticker     = ctx.Parameters.Ticker,
                ReportType = ctx.Parameters.ReportType
            };
            var generatedAddress = r.GenerateAddressRequest();

            var retriever = ContentRetrieverFactory.CreateInstance(ERetrieverType.Yahoo);
            var currentPagehtmlContent = retriever.GetData(generatedAddress);
            var cqHelper = new CQHelper();
            var cq = cqHelper.InitiateWithContent(currentPagehtmlContent);

            // process json inside response
            // find script
            var scripts = cq.Find("script");
            foreach (var s in scripts.Contents())
            {
                var content = s.Render();
                var startIndex = content.IndexOf("root.App.main = ");
                if (startIndex == -1)
                    continue;
                startIndex += 16;

                var endIndex = content.IndexOf("(this));", startIndex);

                var jsonString = content.Substring(startIndex, endIndex - startIndex - 3); // TODO: magic numbers

                jsonString = jsonString.Replace("&quot;", @"""");
                ctx.JsonContentLines.Add(jsonString);
            }


            if (Next != null)
                return Next.Do(ctx);
            else
                return true;
        }
    }
}
