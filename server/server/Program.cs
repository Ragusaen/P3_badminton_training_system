using MySql.Data.MySqlClient;
using Server.DAL;
using Server.Controller;
using Server.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Server.Model.Rules;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using Server.Controller.Network;
using System.Threading;
using Server.Controller.Requests;
using System.Security.Cryptography;
using Server.Controller.Requests.Serialization;
using System.Text;

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            NLog.LogManager.Shutdown();
        }
    }
}
