using application.SystemInterface.Network;
using application.SystemInterface.Requests.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Common.Serialization;

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

        private static T SimpleRequest<T, U>(RequestType requestType, U request) where T : class
        {
            // Serialize request
            Serializer serializer = new Serializer();
            byte[] requestBytes = serializer.Serialize(request);

            // Add request type
            byte[] messageBytes = new byte[requestBytes.Length + 1];
            messageBytes[0] = (byte)requestType;
            Array.Copy(requestBytes, 0, messageBytes, 1, requestBytes.Length);

            // Send request and get response
            byte[] responseBytes = _connection.SendRequest(messageBytes);

            // Deserialize response
            T response = serializer.Deserialize<T>(responseBytes);

            return response;
        }

        public static bool LoginRequest(string username, string password)
        {
            LoginRequest request = new LoginRequest() { username = username, password = password };

            LoginResponse response = SimpleRequest<LoginResponse, LoginRequest>(RequestType.Login, request);

            if (!response.LoginSuccessful)
                return false;
            
            _accessToken = response.token;
            return true;
        }

    }
}
