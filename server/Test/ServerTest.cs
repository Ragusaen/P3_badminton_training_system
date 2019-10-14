using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Server.Controller.Network;
using System.Threading;
using Server.Controller.Requests.Serialization;
using Server.Controller.Requests;

namespace Test
{
    class ServerTest
    {

        [Test]
        public void connection_test()
        {
            byte[] expected = new byte[] { 1, 1 };

            Console.WriteLine(TestContext.CurrentContext.TestDirectory);

            SslTcpServer server = new SslTcpServer("localhost.cer");
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer));
            serverThread.Start();
            while (!server.Running) ;

            client.Connect("localhost", "localhost");
            byte[] payload = new byte[] { 1, (byte)RequestManager.Type.ConnectionTest };

            byte[] actual = client.SendRequest(payload);

            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
