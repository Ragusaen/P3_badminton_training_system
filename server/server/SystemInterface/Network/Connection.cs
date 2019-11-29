using NLog;
using System;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.SystemInterface.Network
{
    /// <summary>
    /// This class abstracts over the connection with a client. It handles the communication with the client.
    /// </summary>
    class Connection
    {
        private static Logger _nlog = LogManager.GetCurrentClassLogger();

        private TcpClient _client;
        private SslStream _sslStream;
        public Connection(TcpClient client, SslStream sslStream)
        {
            _client = client;
            _sslStream = sslStream;
        }

        /// <summary>
        /// Reads requests from the client and sends the data to the request manager.
        /// </summary>
        public void AcceptRequests()
        {
            var requestManager = new RequestManager();

            while (true) // Repeat until the connection drops
            {
                byte[] request = new byte[1];
                try
                {
                    // Read a request
                    request = ReadRequestData(); // Blocks until client writes

                    // Parse the request and handle it
                    var response = requestManager.Parse(request);

                    // Send response to the client
                    Respond(response);

                }
                catch (InvalidRequestException e) // If the request type was unknown
                {
                    _nlog.Error(e, $"Client send a request with an invalid request type: {request[0]}");
                    break;
                }
                catch (System.IO.IOException e) // If the connection was disrupted
                {
                    _nlog.Error(e, "Error reading from client");
                    break;
                }
            }
            Close();
        }

        /// <summary>
        /// Send data to the client
        /// </summary>
        /// <param name="data"> Raw bytes to send to client</param>
        private void Respond(byte[] data)
        {
            // Create the buffer for the data. Add 4 bytes to store the length of the request-
            byte[] response = new byte[data.Length + 4];

            // Convert the request length (int 32 bit) into 4 bytes (8 bit)
            byte[] lengthBytes = BitConverter.GetBytes(data.Length);

            // Copy the 4 bytes that define the length of the request into the beginning of the request
            Array.Copy(lengthBytes, 0, response, 0, lengthBytes.Length);

            // Copy the rest of the request data in afterwards
            Array.Copy(data, 0, response, 4, data.Length);

            // Write the response to the ssl stream
            _sslStream.Write(response);
        }

        public void Close()
        {
            _sslStream.Close();
            _client.Close();
        }

        /// <summary>
        /// Read raw data send from the ssl stream from the client.
        /// </summary>
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

            // Buffer to insert the data into
            byte[] buffer = new byte[requestSize];
            
            // Keep reading until the whole request is read
            int bytesRead = 0;
            while (bytesRead < buffer.Length)
                bytesRead += _sslStream.Read(buffer, bytesRead, buffer.Length - bytesRead);

            // Throw exception if the request was improperly read, or the client send an improper request
            if (bytesRead != requestSize)
            {
                throw new InvalidRequestException("Request was not expected size");
            }

            Console.WriteLine($"SERVER: Read {bytes} bytes");
            return buffer;
        }
    }
}
