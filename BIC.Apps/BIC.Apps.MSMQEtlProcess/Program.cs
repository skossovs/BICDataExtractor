using BIC.Apps.MSMQEtlProcess.Data;
using BIC.Foundation.Interfaces;
using BIC.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQEtlProcess
{
    class Program
    {
        private static ILog _logger = LogServiceProvider.Logger;
        private static CancellationTokenSource _token_source;

        private static string COMMAND_QUEUE = Settings.GetInstance().MsmqNameCommands;
        private static string STATUS_QUEUE  = Settings.GetInstance().MsmqNameStatusEtl;
        private static int    QUEUE_DELAY   = Settings.GetInstance().SleepTimeMsmqReadMsec;

        static int Main(string[] args)
        {
            _token_source = new CancellationTokenSource();
            CancellationToken ct = _token_source.Token;
            try
            {
                using (var mq = new Utils.MSMQ.SenderReciever<CommandMessage, StatusMessage>(COMMAND_QUEUE, STATUS_QUEUE, QUEUE_DELAY))
                {
                    mq.MessageRecievedEvent += ReceiveCommandMessage;
                    mq.StartWatching();
                    _logger.Info("Start Processing..");

                    var task = Task.Run(() =>
                    {
                        IStoppableStatusable<ILog> stoppableStatusable = new StoppableStatusable(mq);
                        using (var processor = new ETL.SqlServer.FileProcessorStoppable(stoppableStatusable))
                        {
                            processor.Do();
                        }
                    }, _token_source.Token);

                    task.Wait(_token_source.Token);
                    mq.StopWatching();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("End Processing with ERRORS..");
                _logger.ReportException(ex);
                return (int)ProcessResult.FATAL;
            }
            finally
            {
                _token_source.Dispose();
            }

            _logger.Info("End Processing SUCCESSFULLY..");
            return (int)ProcessResult.SUCCESS;
        }

        private static void ReceiveCommandMessage(CommandMessage body)
        {
            if (body.ProcessCommand == Foundation.Interfaces.EProcessCommand.Stop)
                _token_source.Cancel();
        }
    }
}
