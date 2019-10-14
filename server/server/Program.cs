using MySql.Data.MySqlClient;
using server.DAL;
using Server.Controller;
using Server.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using Server.Controller.Network;
using System.Threading;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            SslTcpServer server = new SslTcpServer();
            SslTcpClient client = new SslTcpClient();

            Thread serverThread = new Thread(new ThreadStart(server.RunServer));
            serverThread.Start();

            while (!server.Running) ;

            client.Connect("localhost", "localhost");
        }
    }
}
