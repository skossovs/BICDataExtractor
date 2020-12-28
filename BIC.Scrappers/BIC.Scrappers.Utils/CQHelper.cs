using BIC.Utils.Logger;
using BIC.Utils.Monads;
using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Scrappers.Utils
{
    public class CQHelper
    {
        private readonly static object delayLocker = new object();
        public readonly int TimeDelay = Settings.GetInstance().TimeDelayInSeconds * 1000;
        private readonly ILog _logger = LogServiceProvider.Logger;

        public CQ GetData(string url)
        {
            lock (delayLocker)
            {
                _logger.Debug(url);

                var dom = url
                    .TryCatch((u) =>
                    {
                        // TODO: useless sit ups down here. But without them CQ library fails with timeout. There is no help can be googled.
                        CsQuery.Web.CsqWebRequest rq = new CsQuery.Web.CsqWebRequest(u);
                        string result = rq.Get();
                        var dddd = CQ.CreateDocument(result);

                        CsQuery.Web.CsqWebRequest rq1 = new CsQuery.Web.CsqWebRequest(u);
                        string result1 = rq1.Get();
                        var dddd1 = CQ.CreateDocument(result1);

                        return CQ.CreateFromUrl(u); // TODO: only works after useless sit ups
                    }, _logger.ReportException);

                Thread.Sleep(TimeDelay);

                return dom.Do(_ => _logger.Info("Recieved data."));
            }
        }
    }
}
