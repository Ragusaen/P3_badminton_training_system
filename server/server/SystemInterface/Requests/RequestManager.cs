using System;
using application.SystemInterface.Requests.Serialization;
using Common;
using Common.Serialization;
using Server.Controller;

namespace Server.SystemInterface.Requests
{
    class InvalidRequestException : Exception
    {
        public InvalidRequestException(string msg) : base(msg)
        {

        }
    }

    class RequestManager
    {
        public byte[] Response { get; set; }

        public void Parse(byte[] request)
        {
            byte type = request[0];

            byte[] data = new byte[request.Length - 1];
            Array.Copy(request, 1, data, 0, data.Length);
            
            switch ((RequestType)type)
            {
                case RequestType.ConnectionTest:
                    ConnectionTestRequest(data);
                    break;
                case RequestType.Login:
                    LoginRequest(data);
                    break;
                default:
                    throw new InvalidRequestException("Request type was invalid!");
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
            LoginRequest loginRequest = serializer.Deserialize<LoginRequest>(data);
 
            var loginAttempt = new LoginResponse();
            
            // Attempt to login
            loginAttempt.token = user.Login(loginRequest.username, loginRequest.password);
            loginAttempt.LoginSuccessful = loginAttempt.token.Length != 0;

            // Set the response
            Response = serializer.Serialize(loginAttempt);
        }
      
    }
}
