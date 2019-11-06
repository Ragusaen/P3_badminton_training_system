using System;
using Common;
using Common.Serialization;
using Server.SystemInterface.Network;

namespace Server.SystemInterface.Requests
{
    class ClientRequests
    {
        private ClientConnection _conn;

        public ClientRequests(ClientConnection connection) {
            _conn = connection;
        }

        public LoginResponse Login(string username, string password)
        {
            LoginRequest login = new LoginRequest() {Username = username, Password = password};

            var ser = new Serializer();
            byte[] data = ser.Serialize(login);

            byte[] request = ConstructRequest(data, RequestType.Login);
            
            byte[] response = _conn.SendRequest(request);

            LoginResponse loginResponse = ser.Deserialize<LoginResponse>(response);

            return loginResponse;
        }

        private byte[] ConstructRequest(byte[] data, RequestType requestType) {
            byte[] request = new byte[data.Length + 1];
            request[0] = (byte)requestType;
            Array.Copy(data, 0, request, 1, data.Length);

            return request;
        }
    }
}
