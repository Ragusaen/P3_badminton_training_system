﻿using System;
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

        private Dictionary<RequestType, RequestHandler> _requestDictionary =
            new Dictionary<RequestType, RequestHandler>()
            {
                {RequestType.Login, new LoginHandler() },
                {RequestType.CreateAccount, new CreateAccountHandler() },
                {RequestType.GetPracticeTeam, new GetPracticeTeamHandler() },
                //{RequestType.GetAllPlayers, new GetAllPlayersHandler()}
            };

        public byte[] Parse(byte[] request)
        {
            byte type = request[0];

            byte[] data = new byte[request.Length - 1];
            Array.Copy(request, 1, data, 0, data.Length);
            
            var response = _requestDictionary[(RequestType) type].Handle(data);

            return response;
        }

    }
}
