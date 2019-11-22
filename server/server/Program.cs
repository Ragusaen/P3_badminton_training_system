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

        private static void SetPracticeTeam()
        {
            var db = new DatabaseEntities();

            var ps = new practicesession()
            {
                member = db.members.First(),
                focuspoints = new List<focuspoint>()
                {
                    new focuspoint()
                    {
                        Name = "Bagbanespil",
                        Description = "Spil bagerst på din bane"
                    }
                },
                playsession = new playsession()
                {
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1).AddHours(2),
                    Location = "Aalborg Triton"
                },
                focuspoint = new focuspoint()
                {
                    Name = "Smash",
                    Description = "Hit the ball hard"
                },
                practicesessionexercises = new List<practicesessionexercise>()
                {
                    new practicesessionexercise()
                    {
                        exercise = new exercise()
                        {
                            Description = "Run fast",
                            Name = "Fast running"
                        },
                        ExerciseIndex = 0
                    }
                },
                practiceteam = new practiceteam()
                {
                    Name = "Sunday training"
                }
            };

            db.practicesessions.Add(ps);
            db.SaveChanges();
        }

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
