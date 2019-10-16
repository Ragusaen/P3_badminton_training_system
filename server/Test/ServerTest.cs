using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Server.Controller.Network;
using Server.Controller.Requests;
using System.Threading;

namespace Test
{
    [TestClass]
    public class ServerTest
    {
        [TestMethod]
        public void ConnectionTest()
        {
            byte[] expected = new byte[] { 1 };

            SslTcpServer server = new SslTcpServer(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\localhost.cer");
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer));
            serverThread.Start();
            while (!server.Running) ;

            client.Connect("localhost", "localhost");
            byte[] payload = new byte[] { 1, 0, 0, 0, (byte)RequestManager.Type.ConnectionTest };

            byte[] actual = client.SendRequest(payload);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
