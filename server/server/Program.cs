using System;
using System.Linq;
using NLog;
using server.DAL;
using server.Function;
using server.SystemInterface.Network;

namespace server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {

            // If the program is called with the scrape only parameter, it should just scrape and then return
            if (args.Contains("--scrape") || args.Contains("-s"))
            {
                _log.Debug("Starting scraping");
                var scraper = new RankListScraper();
                scraper.UpdatePlayers();

                return;
            }
    
            // Scrape all players if the database is empty
            using(var db = new DatabaseEntities()) {
                if (!db.members.Any())
                {
                    var scraper = new RankListScraper();
                    scraper.UpdatePlayers();
                }
            }
            
            if (true) //args.Contains("--initdb") || args.Contains("-i")
            {
                _log.Debug("Database is being initialized");
                var di = new DatabaseInitializer();
                di.Initialize();
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
