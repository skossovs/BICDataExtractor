﻿using BIC.Apps.MSMQEtlProcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIC.Apps.MSMQEtlProcess
{
    class Program
    {
        private static bool _isInterrupted;
        static int Main(string[] args)
        {
            // TODO: Settings
            using (var mq = new Utils.MSMQ.SenderReciever<CommandMessage, StatusMessage >(".\\Private$\\bic-commands", ".\\Private$\\bic-status-etl", 200))
            {
                mq.MessageRecievedEvent += ReceiveCommandMessage;
                mq.StartWatching();

                // TODO: Fake cycle
                for (int i = 0; i < 1000; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    Console.WriteLine("Ticking..");

                    if(mq.ExceptionLog.Count > 0)
                    {
                        var ex = mq.ExceptionLog.First();
                        Console.WriteLine(ex.Message);
                        mq.ExceptionLog.Remove(ex);
                    }

                    mq.Send(new StatusMessage()
                    {
                        ChannelID     = (int) Foundation.Interfaces.EProcessType.ETL,
                        ProcessStatus = Foundation.Interfaces.EProcessStatus.Running
                    });

                    if (_isInterrupted)
                        break;
                }
                mq.Send(new StatusMessage()
                {
                    ChannelID = (int)Foundation.Interfaces.EProcessType.ETL,
                    ProcessStatus = _isInterrupted ? Foundation.Interfaces.EProcessStatus.Stopped : Foundation.Interfaces.EProcessStatus.Finished
                });
            }

            return (int)BIC.Foundation.Interfaces.ProcessResult.SUCCESS;
        }

        private static void ReceiveCommandMessage(CommandMessage body)
        {
            if(body.ProcessCommand == Foundation.Interfaces.EProcessCommand.Stop)
                _isInterrupted = true;
        }
    }
}
