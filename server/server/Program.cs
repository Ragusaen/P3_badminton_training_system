using MySql.Data.MySqlClient;
using Server.DAL;
using Server.Controller;
using Server.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Server.Model.Rules;
using Server.Model;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using Server.Controller.Network;
using System.Threading;
using Server.Controller.Requests;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            byte[] expected = new byte[] { 1, 1 };

            SslTcpServer server = new SslTcpServer("localhost.cer");
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer));
            serverThread.Start();
            while (!server.Running) ;

            client.Connect("localhost", "localhost");
            byte[] payload = new byte[] { 1, (byte)RequestManager.Type.ConnectionTest };

            byte[] actual = client.SendRequest(payload);
        }
    }
}
