using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Server.Controller.Network
{
    class SslTcpClient
    {        
        TcpClient _tcpClient = null;
        SslStream _sslStream = null;

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }

        public ClientConnection Connect(string machineName, string serverName)
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            _tcpClient = new TcpClient(machineName, 8080);
            Console.WriteLine("Client connected.");

            // Create an SSL stream that will close the client's stream.
            _sslStream = new SslStream(
                _tcpClient.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );

            // The server name must match the name on the server certificate.
            try
            {
                _sslStream.AuthenticateAsClient(serverName);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                _tcpClient.Close();
                return null;
            }

            return new ClientConnection(this, _sslStream);
        }

        public void Disconnect()
        {
            _sslStream.Close();
            _tcpClient.Close();
        }

        
    }
}