using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Server.SystemInterface.Network;
using Server.SystemInterface.Requests;
using System.Threading;
using Server.Controller;
using Common.Serialization;
using Common;

namespace Test
{
    [TestClass]
    public class ServerTest : DBResetter
    {
        private (SslTcpServer server, ClientConnection conn) StartServerAndClient()
        {
            SslTcpServer server = new SslTcpServer(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\localhost.cer");
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer)) { IsBackground = true };
            serverThread.Start();
            while (!server.Running) ;

            ClientConnection conn = client.Connect("localhost", "localhost");

            return (server, conn);
        }

        [TestMethod]
        public void ConnectionTest()
        {
            byte[] expected = new byte[] { 1 };

            var s = StartServerAndClient();
            ClientConnection conn = s.conn;

            byte[] actual = conn.SendRequest(new byte[] { (byte)RequestType.ConnectionTest });

            s.server.Close();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginWith_johninator()
        {
            var s = StartServerAndClient();
            var conn = s.conn;

            var ser = new Serializer();
            byte[] data = ser.Serialize(new LoginRequest() { Username = "johninator", Password = "fortytwo" });
            byte[] request = new byte[data.Length + 1];

            request[0] = (byte)RequestType.Login;
            Array.Copy(data, 0, request, 1, data.Length);

            var response = conn.SendRequest(request);

            LoginResponse la = ser.Deserialize<LoginResponse>(response);

            s.server.Close();

            Assert.AreEqual(true, la.LoginSuccessful);
            Assert.AreEqual(UserManager.TokenSize, la.Token.Length);
        }

    }
}
