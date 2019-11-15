using application.SystemInterface.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Common;
using Common.Model;
using Common.Serialization;
using Common.Model;

namespace application.SystemInterface
{
    static class RequestCreator
    {
        private static ServerConnection _connection = new ServerConnection();

        private static byte[] _accessToken = null;
        public static bool IsLoggedIn => _accessToken != null;

        public static void Connect()
        {
            _connection.Connect();
        }

        private static TResponse SimpleRequest<TRequest, TResponse>(RequestType requestType, TRequest request) where TRequest : Request where TResponse : Response
        {

            Debug.WriteLine("HERE 1");
            // Serialize request
            Serializer serializer = new Serializer();
            byte[] requestBytes = serializer.Serialize(request);


            Debug.WriteLine("HERE 2");

            // Add request type
            byte[] messageBytes = new byte[requestBytes.Length + 1];
            messageBytes[0] = (byte)requestType;
            Array.Copy(requestBytes, 0, messageBytes, 1, requestBytes.Length);


            Debug.WriteLine("HERE 3");

            // Send request and get response
            byte[] responseBytes = _connection.SendRequest(messageBytes);


            Debug.WriteLine("HERE 4");

            // Deserialize response
            TResponse response = serializer.Deserialize<TResponse>(responseBytes);

            return response;
        }

        public static bool LoginRequest(string username, string password)
        {
            LoginRequest request = new LoginRequest() { Username = username, Password = password };

            LoginResponse response = SimpleRequest<LoginRequest, LoginResponse>(RequestType.Login, request);

            if (!response.LoginSuccessful)
                return false;
            
            _accessToken = response.Token;
            return true;
        }

        public static bool CreateAccountRequest(string username, string password, int badmintonId, string name)
        {
            var careq = new CreateAccountRequest()
            {
                Username = username, Password = password
            };

            var response = SimpleRequest<CreateAccountRequest, CreateAccountResponse>(RequestType.CreateAccount, careq);

            return response.WasSuccessful;
        }

        public static List<Player> GetPlayersWithNoAccount()
        {
            var request = new GetPlayersWithNoAccountRequest();

            var response = SimpleRequest<GetPlayersWithNoAccountRequest, GetPlayersWithNoAccountResponse>(
                RequestType.GetPlayersWithNoAccount,
                request);

            return response.Players;

        }

        public static List<FocusPointDescriptor> GetFocusPoints()
        {
            var request = new GetAllFocusPointsRequest();

            var response = SimpleRequest<GetAllFocusPointsRequest, GetAllFocusPointsResponse>(RequestType.GetAllFocusPoints, request);

            return response.FocusPointDescriptors;
        }
    }
}
