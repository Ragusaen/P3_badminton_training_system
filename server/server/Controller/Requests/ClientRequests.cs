using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Controller.Requests.Serialization;
using Server.Controller.Requests;
using Server.Controller.Network;
using System.Net.Security;

namespace Server.Controller.Requests
{
    class ClientRequests
    {
        private ClientConnection _conn;

        public ClientRequests(ClientConnection connection) {
            _conn = connection;
        }

        public LoginAttempt Login(string username, string password)
        {
            LoginData login = new LoginData() {username = username, password = password};

            var ser = new Serializer();
            byte[] data = ser.Serialize(login);

            byte[] request = ConstructRequest(data, RequestManager.Type.Login);
            
            byte[] response = _conn.SendRequest(request);

            LoginAttempt loginAttempt = ser.Deserialize<LoginAttempt>(response);

            return loginAttempt;
        }

        private byte[] ConstructRequest(byte[] data, RequestManager.Type requestType) {
            byte[] request = new byte[data.Length + 1];
            request[0] = (byte)requestType;
            Array.Copy(data, 0, request, 1, data.Length);

            return request;
        }
    }
}
