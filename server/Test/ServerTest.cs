using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Server.Controller.Network;
using System.Threading;
using Server.Controller.Serialization;

namespace Test
{
    class ServerTest
    {

        [Test]
        public void some_server_test()
        {
            SslTcpServer server = new SslTcpServer();
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer));
            serverThread.Start();

            while (!server.Running) ;

            client.Connect("localhost", "localhost");

            LoginData ld = new LoginData() { username = "hansemand", password = "pasword123" };
            var serializer = new Serializer();
            byte[] data = serializer.Serialize(ld);

            byte[] request = new byte[4 + data.Length];

            byte[] length = BitConverter.GetBytes(data.Length);
            Array.Copy(length, 0, request, 0, length.Length);
            Array.Copy(data, 0, request, 4, data.Length);


            byte[] actual = client.SendRequest(request);

            Assert.AreEqual(33, actual.Length);
        }

    }
}
