using NLog;
using Server.SystemInterface.Network;
using System;
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

            //db.members.Find(17).practiceteams.Add(new practiceteam { Name = "Tirsdagstræning" });
            //db.practiceteams.Add(new practiceteam { Name = "Torsdagstræning" });
            //db.SaveChanges();
            RankListScraper scraper = new RankListScraper();
            if (!db.members.Any())
            {
                scraper.UpdatePlayers();
            }
            try
            {
                // var scraper = new RankListScraper();
                // scraper.UpdatePlayers();

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
