using System;
using System.Collections.Generic;
using Common;
using Common.Serialization;
using Server.Controller;
using Server.DAL;
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
                {RequestType.VerifyLineup, new VerifyLineupHandler() },
                {RequestType.GetPracticeTeam, new GetPracticeTeamHandler() },
                {RequestType.GetSchedule, new GetScheduleHandler() },
                {RequestType.GetMemberPracticeTeams, new GetPlayerPracticeTeamsHandler() },
                {RequestType.GetPlayersWithNoAccount, new GetPlayersWithNoAccountHandler() },
                {RequestType.GetAllPlayers, new GetAllPlayersHandler()},
                {RequestType.GetAllTrainers, new GetAllTrainersHandler() },
                {RequestType.GetAllMembers, new GetAllMembersHandler() },
                {RequestType.GetPracticeSession, new GetPracticeSessionHandler() },
                {RequestType.GetPlayerFeedback, new GetPlayerFeedbackHandler() },
                {RequestType.GetPlaySessionFeedback, new GetPlaySessionFeedback() },
                {RequestType.GetPlayerFocusPoints, new GetPlayerFocusPointsHandler() },
                {RequestType.GetTeamMatch, new GetTeamMatchHandler() },
                {RequestType.GetTeamMatchPositions, new GetTeamMatchPositionsHandler()  },
                {RequestType.GetPracticeSessionFocusPoints, new GetPracticeSessionFocusPointsHandler() },
                {RequestType.GetPracticeSessionExercises, new GetPracticeSessionExercisesHandler() },
                {RequestType.GetAllFocusPoints, new GetAllFocusPointDescriptorsHandler() },
                {RequestType.GetPlayer, new GetPlayerHandler() },
                {RequestType.GetTokenMember, new GetTokenMemberHandler() },
                {RequestType.GetAdminPage, new GetAdminPageHandler() },
                {RequestType.GetAllPracticeTeams, new GetAllPracticeTeamsHandler() },
                {RequestType.GetExercises, new GetExerciseHandler() },
                //Setters below
                {RequestType.SetPlayer, new SetPlayerHandler() },
                {RequestType.SetPlayerFocusPoints, new AddPlayerFocusPointHandler() },
                {RequestType.SetComment, new SetCommentHandler() },
                {RequestType.SetPlayerPracticeTeams, new SetPlayerPracticeTeamsHandler() },
                {RequestType.ChangeTrainerPrivileges, new ChangeTrainerPrivilegesHandler() },
                //Deleters below
                {RequestType.DeletePlayerFocusPoint, new DeletePlayerFocusPointsHandler() },
                {RequestType.DeletePlayerPracticeTeam, new DeletePlayerPracticeTeamHandler() },
                {RequestType.DeleteFocusPointDescriptor, new DeleteFocusPointDescriptorHandler() },
                {RequestType.DeletePracticeTeam, new DeletePracticeTeamHandler() },
                //Creators below
                {RequestType.CreateFocusPointDescriptor, new CreateFocusPointDescriptorHandler() },
                {RequestType.SetExerciseDiscriptor, new SetExerciseDescriptorHandler() },
                {RequestType.SetPracticeSession, new SetPracticeSessionHandler() },
                {RequestType.SetTeamMatch, new SetTeamMatchHandler() },
                {RequestType.SetFeedback, new SetFeedbackHandler() },
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
