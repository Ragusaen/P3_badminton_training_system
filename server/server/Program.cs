using NLog;
using Server.SystemInterface.Network;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            var tm = new TeamMatch()
            {
                ID = 0,
                LeagueRound = 0,
                Season = 0,
                Lineup = new Lineup()
                {
                    new Lineup.Group
                    {
                        Type = Lineup.PositionType.MensDouble,
                        Positions = new List<Position>
                    {
                        new Position()
                        {
                            IsExtra = false,
                            OtherIsExtra = false,
                            Player = new Player(),
                            OtherPlayer = new Player()
                        }
                    }}

                }
            };

            var ser = new Serializer();


            

            var db = new DatabaseEntities();
            if (!db.members.Any())
            {
                RankListScraper scraper = new RankListScraper();
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
