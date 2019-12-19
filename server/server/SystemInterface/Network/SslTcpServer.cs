using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using NLog;

namespace server.SystemInterface.Network
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

        /// <summary>
        /// Run the server and accept clients.
        /// </summary>
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
                    // Start a new connection with the client
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

        /// <summary>
        /// Authenticate client and start the connection
        /// </summary>
        private void StartConnection(TcpClient client)
        {
            // Create the SSL stream
            SslStream sslStream = new SslStream(client.GetStream(), false);

            try
            {
                // Authenticate the server but don't require the client to authenticate.
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

            // Create connection
            Connection connection = new Connection(client, sslStream);

            // Start the connection in a new thread
            Thread t = new Thread(connection.AcceptRequests) { IsBackground = true };
            _threads.Add(t);
            t.Start();

            _log.Debug("Connection established.");
            // Go back to accepting clients
        }

    }
}