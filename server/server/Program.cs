using NLog;
using Server.SystemInterface.Network;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using Common.Model;
using Common.Serialization;
using Server.Controller;
using Server.DAL;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var db = new DatabaseEntities();

            if (!db.members.Any())
            {
                RankListScraper scraper = new RankListScraper();
                scraper.UpdatePlayers();
            }
            
            try
            {

                _log.Debug("Server started");
                SslTcpServer sslTcpServer = new SslTcpServer("cert.pfx");
                sslTcpServer.RunServer();
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    _log.Error(e.InnerException.Message);
                _log.Error(e, e.ToString());
                throw;
            }

            NLog.LogManager.Shutdown();
        }
    }
}
