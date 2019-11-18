using NLog;
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

            var req = new GetPlayersWithNoAccountRequest();
            var ser = new Serializer();
            byte[] a = ser.Serialize(req);

            ser.Deserialize<GetPlayersWithNoAccountRequest>(a);

            try
            {
                SslTcpServer sslTcpServer = new SslTcpServer("localhost.cer");
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
