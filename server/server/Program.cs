using NLog;
using Server.SystemInterface.Network;
using System;
using System.Data.Entity;
using System.Linq;
using Common.Model;
using Server.DAL;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var db = new p3_dbEntities();

            db.members.Add(new member {Name = "John Larsen", Sex = (int)Sex.Male, BadmintonPlayerID = 02221208, MemberType = 0});

            db.SaveChanges();

            return;
            try
            {
                SslTcpServer sslTcpServer = new SslTcpServer("localhost.cer");
                sslTcpServer.RunServer();
            } catch (Exception e)
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
