using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security;
using Server.Controller;
using System.Threading;
using Server.Controller.Network;

namespace Server.Controller.Network
{
    public sealed class SslTcpServer
    {
        // The certificate for SSL/TSL communication
        private X509Certificate _serverCertificate = null;
        private string _certificatePath;

        public bool Running { get; private set; } = false;

        public SslTcpServer(string certificatePath)
        {
            _certificatePath = certificatePath;
        }

        public void RunServer()
        {
            Console.WriteLine("Running server!");
            // Create the server certificate
            _serverCertificate = new X509Certificate(_certificatePath);

            // Create the socket and listen to it, this accepts any IP
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();

            // Accept clients 
            Running = true;
            while (Running)
            {
                Console.WriteLine("Waiting for a client to connect...");

                // Listen to port and block until client connects
                TcpClient client = listener.AcceptTcpClient();

                // Process the client
                StartConnection(client);
            }
        }

        private void StartConnection(TcpClient client)
        {
            // Create the SSL stream
            SslStream sslStream = new SslStream(client.GetStream(), false);

            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(_serverCertificate, clientCertificateRequired: false, checkCertificateRevocation: true);

                // Set timeouts for the read and write to 5 seconds.
                sslStream.ReadTimeout = 100000;
                sslStream.WriteTimeout = 100000;
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return;
            }

            Connection connection = new Connection(client, sslStream);

            Thread t = new Thread(new ThreadStart(connection.AcceptRequests));
        }

    }
}