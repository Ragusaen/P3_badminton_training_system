using NLog;
using Server.SystemInterface.Requests;
using System;
using System.Net.Security;
using System.Net.Sockets;

namespace Server.SystemInterface.Network
{
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

        public void AcceptRequests()
        {
            bool isOpen = true;
            while (isOpen)
            {
                byte[] request;
                try
                {
                    // Wait for request
                    request = ReadRequestData();
                    RequestManager reqman = new RequestManager();

                    try
                    {
                        reqman.Parse(request);
                    } catch (InvalidRequestException e)
                    {
                        _nlog.Error(e, $"Client send a request with an invalid request type: {request[0]}");
                        reqman.Response = new byte[] { 0 };
                    }
                    Respond(reqman.Response);
                }
                catch (InvalidRequestException)
                {
                    _sslStream.Flush();
                    continue;
                }
            }
        }

        private void Respond(byte[] data)
        {
            byte[] response = new byte[data.Length + 4];
            byte[] lengthBytes = BitConverter.GetBytes(data.Length);
            Array.Copy(lengthBytes, 0, response, 0, lengthBytes.Length);

            Array.Copy(data, 0, response, 4, data.Length);

            _sslStream.Write(response);
            Console.WriteLine($"SERVER: Wrote {response.Length}");
        }

        public void Close()
        {
            _sslStream.Close();
            _client.Close();
        }

        private byte[] ReadRequestData()
        {
            // Read first 4 bytes, which is the size of the request
            byte[] request_size_buffer = new byte[4];
            int bytes = _sslStream.Read(request_size_buffer, 0, request_size_buffer.Length);
            if (bytes != 4)
            {
                throw new InvalidRequestException("Request was smaller than 4 bytes");
            }

            // Convert to int (byte order is big endian)
            int request_size = BitConverter.ToInt32(request_size_buffer, 0);

            byte[] buffer = new byte[request_size];
            bytes = _sslStream.Read(buffer, 0, buffer.Length);
            
            if (bytes != request_size)
            {
                throw new InvalidRequestException("Request was not expected size");
            }
            Console.WriteLine($"SERVER: Read {bytes} bytes");
            return buffer;
        }
    }
}
