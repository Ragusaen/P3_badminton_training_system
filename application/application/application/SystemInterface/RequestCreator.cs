using application.SystemInterface.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Common;
using Common.Model;
using Common.Serialization;


namespace application.SystemInterface
{
    static class RequestCreator
    {
        private static ServerConnection _connection = new ServerConnection();

        private static byte[] _accessToken = null;
        public static bool IsLoggedIn => _accessToken != null;

        public static Member LoggedInMember;

        public static bool Connect()
        {
            return _connection.Connect();
        }

        private static TResponse SimpleRequest<TRequest, TResponse>(RequestType requestType, TRequest request) where TRequest : Request where TResponse : Response
        {
            //Add access token
            if (request is PermissionRequest permissionRequest)
                permissionRequest.Token = _accessToken;

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
            TResponse response = serializer.Deserialize<TResponse>(responseBytes);

            return response;
        }

        public static bool LoginRequest(string username, string password)
        {
            LoginRequest request = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            LoginResponse response = SimpleRequest<LoginRequest, LoginResponse>(RequestType.Login, request);

            if (response.LoginSuccessful)
                _accessToken = response.Token;

            return response.LoginSuccessful;
        }

        public static bool CreateAccountRequest(string username, string password, int badmintonId, string name)
        {
            var careq = new CreateAccountRequest()
            {
                Username = username,
                Password = password,
                AddAsPlayer = name == null,
                BadmintonPlayerId = badmintonId,
                Name = name
            };

            var response = SimpleRequest<CreateAccountRequest, CreateAccountResponse>(RequestType.CreateAccount, careq);

            return response.WasSuccessful;
        }

        public static List<Player> GetPlayersWithNoAccount()
        {
            var request = new GetPlayersWithNoAccountRequest();

            var response =
                SimpleRequest<GetPlayersWithNoAccountRequest, GetPlayersWithNoAccountResponse>(
                    RequestType.GetPlayersWithNoAccount, request);

            return response.Players;
        }

        public static List<FocusPointDescriptor> GetFocusPoints()
        {
            var request = new GetAllFocusPointsRequest();
            
            var response = SimpleRequest<GetAllFocusPointsRequest, GetAllFocusPointsResponse>(RequestType.GetAllFocusPoints, request);

            return response.FocusPointDescriptors;
        }

        public static List<Feedback> GetPlayerFeedback()
        {
            var request = new GetPlayerFeedbackRequest();

            var response = SimpleRequest<GetPlayerFeedbackRequest, GetPlayerFeedbackResponse>(RequestType.GetPlayerFeedback, request);

            return response.Feedback;
        }

        public static Player GetPlayer(int id)
        {
            var request = new GetPlayerRequest
            {
                Id = id
            };

            var response = SimpleRequest<GetPlayerRequest, GetPlayerResponse>(RequestType.GetPlayer, request);

            return response.Player;
        }

        public static List<FocusPointItem> GetPlayerFocusPointItems(int memberId)
        {
            var request = new GetPlayerFocusPointsRequest
            {
                MemberId = memberId
            };

            var response =
                SimpleRequest<GetPlayerFocusPointsRequest, GetPlayerFocusPointsResponse>(
                    RequestType.GetPlayerFocusPoints, request);

            var result = response.FocusPoints.Select(p => new FocusPointItem {Descriptor = p}).ToList();

            return result;
        }

        public static Member GetLoggedInMember()
        {
            var request = new GetTokenMemberRequest();

            var response =
                SimpleRequest<GetTokenMemberRequest, GetTokenMemberResponse>(RequestType.GetTokenMember,
                    request);

            return response.Member;
        }


        public static List<PracticeTeam> GetMemberPracticeTeams(Member member)
        {
            var request = new GetMemberPracticeTeamRequest
            {
                Member = member
            };

            var response =
                SimpleRequest<GetMemberPracticeTeamRequest, GetMemberPracticeTeamResponse>(
                    RequestType.GetMemberPracticeTeams, request);

            return response.PracticeTeams;
        }

        public static List<PlaySession> GetSchedule()
        {
            var request = new GetScheduleRequest();

            var response = SimpleRequest<GetScheduleRequest, GetScheduleResponse>(RequestType.GetSchedule, request);

            var list = new List<PlaySession>();
            list.AddRange(response.Matches);
            list.AddRange(response.PracticeSessions);

            return list;
        }

        // Setters below
        public static bool SetPlayer()
        {
            var request = new SetPlayerRequest();

            var response = SimpleRequest<SetPlayerRequest, SetPlayerResponse>(RequestType.SetPlayer, request);

            return response.WasSuccessful;
        }

        public static bool SetPlayerFocusPoints(Player player, List<FocusPointItem> focusPointItems)
        {
            var request = new SetPlayerFocusPointsRequest
            {
                Player = player,
                FocusPoints = focusPointItems
            };

            var response =
                SimpleRequest<SetPlayerFocusPointsRequest, SetPlayerFocusPointsResponse>(
                    RequestType.SetPlayerFocusPoints, request);

            return response.WasSuccessful;
        }

        public static void SetComment(Member member, string comment)
        {
            var request = new SetCommentRequest()
            {
                Member = member,
                NewComment = comment
            };

            var response = SimpleRequest<SetCommentRequest, SetCommentResponse>(RequestType.SetComment, request);
        }

        // Deleters below

        public static bool DeletePlayerFocusPoints(int memberId, FocusPointItem focusPointItem)
        {
            var request = new DeletePlayerFocusPointRequest
            {
                MemberId = memberId,
                FocusPointId = focusPointItem.Descriptor.Id
            };

            var response =
                SimpleRequest<DeletePlayerFocusPointRequest, DeletePlayerFocusPointResponse>(
                    RequestType.DeletePlayerFocusPoints, request);

            return response.WasSuccessful;
        }

        // creators below
        public static FocusPointDescriptor CreateFocusPointDescriptor(FocusPointDescriptor fp)
        {
            var request = new CreateFocusPointDescriptorRequest
            {
                FocusPointDescriptor = fp
            };

            var response =
                SimpleRequest<CreateFocusPointDescriptorRequest, CreateFocusPointDescriptorResponse>(
                    RequestType.CreateFocusPointDescriptor, request);

            return response.FocusPointDescriptor;
        }
    }
}
