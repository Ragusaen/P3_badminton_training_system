using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server.Controller.Requests;

namespace Server.Controller.Network
{
    class Connection
    {
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
                    reqman.Parse(request);
                    Respond(reqman.Response);
                }
                catch (InvalidRequestException e)
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

            byte[] buffer = new byte[request_size - 4];
            bytes = _sslStream.Read(buffer, 0, buffer.Length);
            
            if (bytes != request_size - 4)
            {
                throw new InvalidRequestException("Request was not expected size");
            }

            return buffer;
        }
    }
}
