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
            Serializer ser = new Serializer();

            Member member = new Member()
            {
                Id = 1,
                Name = "Hans Andersen"
            };

            var asd = ser.Serialize(member);

            Member n = ser.Deserialize<Member>(asd);

            Console.WriteLine($"Id: {n.Id}, {n.Name}");


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
