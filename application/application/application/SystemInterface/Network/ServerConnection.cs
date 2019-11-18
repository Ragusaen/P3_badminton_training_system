using System;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace application.SystemInterface.Network
{
    class ServerConnection
    {
        private readonly IPAddress _machineName = new IPAddress(new byte[] { 192,168,42,59 });
        private readonly string _serverName = "cert";

        private TcpClient _tcpClient = null;
        private SslStream _sslStream = null;

        public bool IsConnected => (_tcpClient != null && _sslStream != null) && (_tcpClient.Connected && _sslStream.IsAuthenticated);

        public void Connect()
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            _tcpClient = new TcpClient();
            var connectTask = _tcpClient.ConnectAsync(_machineName, 8080);

            // Wait one second for connection
            connectTask.Wait(TimeSpan.FromSeconds(1));

            if (!connectTask.IsCompleted)
            {
                throw new FailedToConnectToServerException("Failed to connect to server!");
            }

            Debug.WriteLine("Client connected.");

            // Create an SSL stream that will close the client's stream.
            _sslStream = new SslStream(
                _tcpClient.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );

            // The server name must match the name on the server certificate.
            var authTask = _sslStream.AuthenticateAsClientAsync(_serverName);

            authTask.Wait(TimeSpan.FromSeconds(10));

            if (!authTask.IsCompleted)
            {
                Debug.WriteLine("Authentication failed - closing the connection.");
                _tcpClient.Close();
                throw new AuthenticationException("Could not authenticate");
            }

            Debug.WriteLine("Successfully authenticated!");
        }

        public byte[] SendRequest(byte[] data)
        {
            if (!IsConnected)
                throw new NotConnectedToServerException("Couldn't send request as there was no connection to server.");

            byte[] request = new byte[data.Length + 4];
            byte[] lengthBytes = BitConverter.GetBytes(data.Length);
            Array.Copy(lengthBytes, 0, request, 0, lengthBytes.Length);

            Array.Copy(data, 0, request, 4, data.Length);

            _sslStream.Write(request);
            Debug.WriteLine($"CLIENT: Wrote {request.Length} bytes");

            byte[] received = ReadRequestData();

            Debug.WriteLine($"CLIENT: Read {received.Length} bytes");
            return received;
        }

        private byte[] ReadRequestData()
        {
            // Read first 4 bytes, which is the size of the request
            byte[] requestSizeBuffer = new byte[4];
            int bytes = _sslStream.Read(requestSizeBuffer, 0, requestSizeBuffer.Length);
            if (bytes != 4)
            {
                throw new InvalidRequestException("Request was smaller than 4 bytes");
            }

            // Convert to int (byte order is big endian)
            int requestSize = BitConverter.ToInt32(requestSizeBuffer, 0);

            byte[] buffer = new byte[requestSize];
            bytes = _sslStream.Read(buffer, 0, buffer.Length);

            if (bytes != requestSize)
            {
                throw new InvalidRequestException("Request was not expected size");
            }

            return buffer;
        }

        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            return true; // Accepts anything
        }
    }
}
