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

namespace Server
{
    class Program
    {
        public static void Main(string[] args)
        {
            var byts = new byte[128];
            byts[0] = 0x42;
            var pkbdf2 = new Rfc2898DeriveBytes("fortytwo", byts, 100000);

            byte[] pw_hash = pkbdf2.GetBytes(32);

            string s = BitConverter.ToString(pw_hash).Replace("-", "");

            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
