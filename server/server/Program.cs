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

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var parser = new Parser();

            try
            {
                parser.UpdatePlayers();
            }

            catch (Exception e)
            {
                _log.Error(e, e.Message);
                throw;
            }
            NLog.LogManager.Shutdown();
        }
    }
}
