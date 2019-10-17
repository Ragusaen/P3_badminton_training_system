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
using NLog;
using Server.Controller.Requests;
using System.Security.Cryptography;
using Server.Controller.Requests.Serialization;
using System.Text;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var parser = new Parser();
            var pdao = new PlayerDAO();

            try
            {
                parser.UpdatePlayers();
                var o = pdao.ReadAll().ToList();
                var p = o[0];
                Console.WriteLine(p);
                Console.WriteLine(p.Rankings);
                Console.ReadKey();
            }

            catch (Exception e)
            {
                _log.Error(e, e.ToString());
                throw;
            }

            NLog.LogManager.Shutdown();
        }
    }
}
