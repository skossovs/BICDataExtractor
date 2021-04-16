using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BIC.Utils.MSMQ;
using System.Threading;

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
            //Action<StatusTest> Sr_MessageRecievedEvent = (body) => Assert.AreEqual("0", body.Status); // TODO: syntax error
            // send c to s
            using (var sr = new MSMQ.SenderReciever<CommandTest, StatusTest>(".\\Private$\\bic-commands", ".\\Private$\\bic-status"))
            {
                sr.Send(new StatusTest() { Status = "0" });
                Thread.Sleep(2000);
            }

            // recieve
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>(".\\Private$\\bic-status", ".\\Private$\\bic-commands"))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEvent;
                rs.StartWatching();
                Thread.Sleep(2000);
            }
        }

        [TestMethod]
        public void TestRecievingOnly()
        {
            //Action<StatusTest> Sr_MessageRecievedEvent = (body) => Assert.AreEqual("0", body.Status); // TODO: syntax error
            // recieve
            using (var rs = new MSMQ.SenderReciever<StatusTest, CommandTest>(".\\Private$\\bic-status", ".\\Private$\\bic-commands"))
            {
                rs.MessageRecievedEvent += Sr_MessageRecievedEvent;
                rs.StartWatching();
                rs.StopWatching();
            }
        }

        private void Sr_MessageRecievedEvent(StatusTest body)
        {
            Assert.AreEqual("0", body.Status);
        }
    }
}
