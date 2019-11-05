﻿using NLog;
using Server.SystemInterface.Network;
using System;

namespace Server
{
    class Program
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
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
