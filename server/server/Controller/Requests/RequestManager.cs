using Server.Controller.Network;
using Server.Controller.Requests.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller.Requests
{
    class RequestManager
    {
        public byte[] Response { get; set; }

        public enum Type { ConnectionTest, Login }

        public void Parse(byte[] request)
        {
            byte type = request[0];

            byte[] data = new byte[request.Length - 1];
            Array.Copy(request, 1, data, 0, data.Length);
            
            switch ((Type)type)
            {
                case Type.ConnectionTest:
                    ConnectionTestRequest(data);
                    break;
                case Type.Login:
                    LoginRequest(data);
                    break;
                default:
                    throw new ArgumentException("Request type was invalid!");
            }
        }

        private void ConnectionTestRequest(byte[] data)
        {
            Response = new byte[] { 1 };
        }

        private void LoginRequest(byte[] data)
        {
            // User object for user login
            User user = new User();

            var serializer = new Serializer();
            
            // Get the data
            LoginData loginData = serializer.Deserialize<LoginData>(data);
 
            var loginAttempt = new LoginAttempt();
            
            // Attempt to login
            loginAttempt.token = user.Login(loginData.username, loginData.password);
            loginAttempt.LoginSuccessful = loginAttempt.token.Length != 0;

            // Set the reponse
            Response = serializer.Serialize(loginAttempt);
        }
      
    }
}
