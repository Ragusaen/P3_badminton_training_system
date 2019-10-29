using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Server.Controller.Network;
using Server.Controller.Requests;
using System.Threading;
using Server.Controller.Requests.Serialization;
using System.Text;
using Server.Controller;

using System.Diagnostics;

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

            byte[] actual = conn.SendRequest(new byte[] { (byte)RequestManager.Type.ConnectionTest });

            s.server.Close();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoginWith_johninator()
        {
            var s = StartServerAndClient();
            var conn = s.conn;

            var ser = new Serializer();
            byte[] data = ser.Serialize(new LoginData() { username = "johninator", password = "fortytwo" });
            byte[] request = new byte[data.Length + 1];

            request[0] = (byte)RequestManager.Type.Login;
            Array.Copy(data, 0, request, 1, data.Length);

            var response = conn.SendRequest(request);

            LoginAttempt la = ser.Deserialize<LoginAttempt>(response);

            s.server.Close();

            Assert.AreEqual(true, la.LoginSuccessful);
            Assert.AreEqual(User.tokenSize, la.token.Length);
        }

        [TestMethod]
        public void LoginWith_nonexistant()
        {
            var s = StartServerAndClient();
            ClientRequests cr = new ClientRequests(s.conn);

            LoginAttempt la = cr.Login("someuserthatdoesnotexist", "hypnotoad");

            s.server.Close();

            Assert.IsFalse(la.LoginSuccessful);
            Assert.AreEqual(0, la.token.Length);
        }

    }
}
