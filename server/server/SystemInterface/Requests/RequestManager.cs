using System;
using System.Collections.Generic;
using Common;
using Common.Serialization;
using Server.Controller;
using Server.SystemInterface.Requests.Handlers;

namespace Server.SystemInterface.Requests
{
    class InvalidRequestException : Exception
    {
        public InvalidRequestException(string msg) : base(msg)
        {

        }
    }

    delegate TResponse RequestHandlerDelegate<out TResponse, in TRequest>(TRequest request);

    class RequestManager
    {
        public byte[] Response { get; set; }

        private Dictionary<RequestType, RequestHandler> _requestDictionary =
            new Dictionary<RequestType, RequestHandler>()
            {
                {RequestType.Login, new LoginHandler() },
                {RequestType.CreateAccount, new CreateAccountHandler() },
                //{RequestType.GetSchedule, new  }
            };

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
                case RequestType.CreateAccount:
                default:
                    throw new InvalidRequestException("Request type was invalid!");
            }
        }

        private void ConnectionTestRequest(byte[] data)
        {
            Response = new byte[] { 1 };
        }

        private static LoginResponse Login(LoginRequest loginRequest)
        {
            UserManager userManager = new UserManager();
            var loginAttempt = new LoginResponse();

            // Attempt to login
            loginAttempt.Token = userManager.Login(loginRequest.Username, loginRequest.Password);
            loginAttempt.LoginSuccessful = loginAttempt.Token.Length != 0;

            return loginAttempt;
        }

        private void LoginRequest(byte[] data)
        {
            // UserManager object for userManager login
            UserManager userManager = new UserManager();

            var serializer = new Serializer();

            // Get the data
            LoginRequest loginRequest = serializer.Deserialize<LoginRequest>(data);
 
            var loginAttempt = new LoginResponse();
            
            // Attempt to login
            loginAttempt.Token = userManager.Login(loginRequest.Username, loginRequest.Password);
            loginAttempt.LoginSuccessful = loginAttempt.Token.Length != 0;

            // Set the response
            Response = serializer.Serialize(loginAttempt);
        }
      
    }
}
