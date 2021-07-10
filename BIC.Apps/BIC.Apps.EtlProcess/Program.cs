using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Apps.EtlProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            var task = Task.Run(() =>
            {
                using (var processor = new ETL.SqlServer.FileProcessor())
                {
                    processor.Do();
                }
            }, tokenSource.Token);

            // press any key to stop processing
            System.Console.ReadLine();

            tokenSource.Cancel();
            System.Console.WriteLine("Processing has been cancelled");

            try
            {
                task.Wait(ct);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource.Dispose();
            }
        }
    }
}
