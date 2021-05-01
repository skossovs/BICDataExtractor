using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Utils.MSMQ;
using System.Threading;
using System.Collections.Generic;

namespace BIC.Utils.Tests
{
    public class StatusTest : Signal
    {
        public string Status;
    }

    public class CommandTest : Signal
    {
        public string Command;
    }

    [TestClass]
    public class TestMSMQ
    {
        /// <summary>
        /// TODO: automate queue creation in Computer Management->Message Queueing->Private Queues
        /// </summary>
        [TestMethod]
        public void TestSendingRecievingStatusZero()
        {
            // send c to s
            using (var sr = new MSMQ.SenderReciever<CommandTest, StatusTest>(".\\Private$\\bic-commands", ".\\Private$\\bic-status", 200))
            {
                sr.Send(new StatusTest() { Status = "0" });
            }

            // recieve
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>(".\\Private$\\bic-status", ".\\Private$\\bic-commands", 200))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEvent;
                rs.StartWatching();
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void TestRecievingOnly()
        {
            // recieve
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>(".\\Private$\\bic-status", ".\\Private$\\bic-commands", 200))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEvent;
                rs.StartWatching();
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void TestOneToManySendingReceivingStatusZero()
        {
            var receivers = new Dictionary<int, string>()
            {
                {0, ".\\Private$\\bic-status-etl" },
                {1, ".\\Private$\\bic-status-scrap" }
            };

            // send c to s
            using (var sr = new MSMQ.OneToManySenderReceiver<CommandTest, StatusTest>(200, receivers, ".\\Private$\\bic-commands"))
            {
                sr.Send(new StatusTest() { ChannelID = 0, Status = "0" });
                sr.Send(new StatusTest() { ChannelID = 1, Status = "0" });
            }

            // recieve 1st
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>(".\\Private$\\bic-commands", ".\\Private$\\bic-status-etl", 200))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEventChannel0;
                rs.StartWatching();
                Thread.Sleep(2000);
                Assert.AreEqual(0, rs.ExceptionLog.Count);
            }

            // recieve 2d
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>( ".\\Private$\\bic-commands", ".\\Private$\\bic-status-scrap", 200))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEventChannel1;
                rs.StartWatching();
                Thread.Sleep(2000);
                Assert.AreEqual(0, rs.ExceptionLog.Count);
            }

        }

        private void Sr_MessageRecievedEvent(StatusTest body)
        {
            Assert.AreEqual("0", body.Status);
        }

        private void Sr_MessageRecievedEventChannel0(StatusTest body)
        {
            Assert.AreEqual(0, body.ChannelID);
        }

        private void Sr_MessageRecievedEventChannel1(StatusTest body)
        {
            Assert.AreEqual(1, body.ChannelID);
        }


    }
}
