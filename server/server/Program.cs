﻿using NLog;
using Server.SystemInterface.Network;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using Common.Model;
using Common.Serialization;
using Server.Controller;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {

            try
            {
                var scraper = new RankListScraper();
                scraper.UpdatePlayers();

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
