﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Server.Controller.Network;
using System.Threading;
using Server.Controller.Requests.Serialization;
using Server.Controller.Requests;

using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Test
{
    [TestFixture]
    [Parallelizable]
    class ServerTest
    {
        [Test]
        public void connection_test()
        {
            byte[] expected = new byte[] { 1 };
            
            SslTcpServer server = new SslTcpServer(TestContext.CurrentContext.TestDirectory + "\\localhost.cer");
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
