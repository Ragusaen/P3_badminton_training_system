using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security;
using Server.Controller;
using System.Threading;
using System.Collections.Generic;
using NLog;

namespace Server.SystemInterface.Network
{

    sealed class SslTcpServer
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        private List<Thread> _threads = new List<Thread>();

        // The certificate for SSL/TSL communication
        private X509Certificate2 _serverCertificate = null;
        private string _certificatePath;

        private TcpListener listener = null;
        private List<TcpClient> clients = new List<TcpClient>();

        public bool Running { get; private set; } = false;

        public SslTcpServer(string certificatePath)
        {
            _certificatePath = certificatePath;
        }

        public void RunServer()
        {
            // Create the server certificate
            _serverCertificate = new X509Certificate2(_certificatePath, "johnBob174!ba_");

            // Create the socket and listen to it, this accepts any IP
            listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            // Accept clients 
            Running = true;
            while (Running)
            {
                // Listen to port and block until client connects
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);

                _log.Debug("Client accepted.");

                // Process the client
                try
                {
                    StartConnection(client);
                } catch (IOException e)
                {
                    _log.Error(e, e.ToString());
                }
            }
        }

        public void Close()
        {
            Running = false;
            _threads.ForEach(t => t.Abort());
            listener.Stop();
            clients.ForEach(c => c.Close());
        }

        private void StartConnection(TcpClient client)
        {
            // Create the SSL stream
            SslStream sslStream = new SslStream(client.GetStream(), false);

            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(_serverCertificate, clientCertificateRequired: false, checkCertificateRevocation: true);
            }
            catch (AuthenticationException e)
            {
                _log.Error("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    _log.Error("Inner exception: {0}", e.InnerException.Message);
                }
                _log.Error("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return;
            }

            Connection connection = new Connection(client, sslStream);

            Thread t = new Thread(new ThreadStart(connection.AcceptRequests)) { IsBackground = true };
            _threads.Add(t);
            t.Start();

            _log.Debug("Connection established.");
        }

    }
}