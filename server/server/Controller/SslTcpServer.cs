using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace server.Controller
{
    class SslTcpServer
    {
        private X509Certificate certificate = null;

        public void Run(string certificate_path)
        {
            certificate = X509Certificate.CreateFromCertFile(certificate_path);

            // Create socket
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Waiting for connection...");

                TcpClient client = listener.AcceptTcpClient();
                ProcessClient(client);
            }
        }

        public void ProcessClient(TcpClient client)
        {
            SslStream sslStream = new SslStream(client.GetStream(), false);

            try
            {
                sslStream.AuthenticateAsServer(certificate, clientCertificateRequired: false, checkCertificateRevocation: true);

                Console.WriteLine("Waiting for client message...");
                string data = ReadMsg(sslStream);

            } catch (AuthenticationException e)
            {

            }
        }

        private void ReadMsg(SslStream sslStream)
        {
            byte[] buffer = new byte[2048];
            StringBuilder msgData = new StringBuilder();
            int bytes = -1;

            do
            {
                // Read the bytes
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Decode as utf-8
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                msgData.Append(chars)

            } while (bytes != 0);

        }
    }
}
