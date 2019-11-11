using NLog;
using Server.SystemInterface.Network;
using System;
using Common.Model;
using Server.DAL;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var md = new MemberDAO();
            var b = md.Create(null, MemberRole.Type.Trainer, "Hans Peter", Sex.Male, null);

            Console.WriteLine(b);
            Console.ReadKey();

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
