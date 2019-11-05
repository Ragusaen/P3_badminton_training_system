using Server.SystemInterface.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemInterface.Network
{
    class ClientConnection
    {
        private SslTcpClient _sslTcpClient;
        private SslStream _sslStream;

        public ClientConnection(SslTcpClient client, SslStream stream)
        {
            _sslStream = stream;
            _sslTcpClient = client;
        }

        public byte[] SendRequest(byte[] data)
        {
            byte[] request = new byte[data.Length + 4];
            byte[] lengthBytes = BitConverter.GetBytes(data.Length);
            Array.Copy(lengthBytes, 0, request, 0, lengthBytes.Length);

            Array.Copy(data, 0, request, 4, data.Length);

            _sslStream.Write(request);
            Console.WriteLine($"CLIENT: Wrote {request.Length} bytes");

            byte[] received = ReadRequestData();

            Console.WriteLine($"CLIENT: Read {received.Length} bytes");
            return received;
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

            return buffer;
        }
    }
}
