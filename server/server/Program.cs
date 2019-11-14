using NLog;
using Server.SystemInterface.Network;
using System;
using System.Data.Entity;
using System.Linq;
using Common.Model;
using Common.Serialization;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var p = new Server.Controller.Parser();
            try
            {
                p.UpdatePlayers();
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                throw;
            }

            NLog.LogManager.Shutdown();
        }
    }
}
