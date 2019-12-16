using application.SystemInterface.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using application.UI;
using Common;
using Common.Model;
using Common.Serialization;
using Xamarin.Forms;


namespace application.SystemInterface
{
    public class RequestFailedException : Exception {
        public RequestFailedException(string msg) : base(msg)
        {

        }
    }

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

        private static TResponse SendRequest<TRequest, TResponse>(RequestType requestType, TRequest request)
            where TRequest : Request where TResponse : Response
        {
            //Add access token
            if (request is PermissionRequest permissionRequest)
                permissionRequest.Token = _accessToken;

            // Serialize request
            Serializer serializer = new Serializer();

            byte[] requestBytes = serializer.Serialize(request);

            // Add request type
            byte[] messageBytes = new byte[requestBytes.Length + 1];
            messageBytes[0] = (byte) requestType;
            Array.Copy(requestBytes, 0, messageBytes, 1, requestBytes.Length);


            // Send request and get response
            byte[] responseBytes = _connection.WriteRequest(messageBytes);

            // Deserialize response
            TResponse response = serializer.Deserialize<TResponse>(responseBytes);

            if (response.Error != null)
            {
                throw new RequestFailedException(response.Error);
            }

            return response;
        }

        internal static List<ExerciseDescriptor> GetExercises()
        {
            var request = new GetExercisesRequest();
            var response = SendRequest<GetExercisesRequest, GetExercisesResponse>(RequestType.GetExercises, request);
            return response.Exercises;
        }

        public static bool LoginRequest(string username, string password)
        {
            LoginRequest request = new LoginRequest()
            {
                Username = username,
                Password = password
            };

            LoginResponse response = SendRequest<LoginRequest, LoginResponse>(RequestType.Login, request);

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

            var response = SendRequest<CreateAccountRequest, CreateAccountResponse>(RequestType.CreateAccount, careq);

            return response.WasSuccessful;
        }

        public static List<Player> GetPlayersWithNoAccount()
        {
            var request = new GetPlayersWithNoAccountRequest();

            var response =
                SendRequest<GetPlayersWithNoAccountRequest, GetPlayersWithNoAccountResponse>(
                    RequestType.GetPlayersWithNoAccount, request);

            return response.Players;
        }

        public static List<FocusPointDescriptor> GetFocusPoints()
        {
            var request = new GetAllFocusPointsRequest();
            
            var response = SendRequest<GetAllFocusPointsRequest, GetAllFocusPointsResponse>(RequestType.GetAllFocusPoints, request);

            return response.FocusPointDescriptors;
        }

        public static List<Feedback> GetPlayerFeedback(Member member)
        {
            var request = new GetPlayerFeedbackRequest
            {
                MemberId = member.Id
            };

            var response = SendRequest<GetPlayerFeedbackRequest, GetPlayerFeedbackResponse>(RequestType.GetPlayerFeedback, request);

            return response.Feedback;
        }

        public static Player GetPlayer(int id)
        {
            var request = new GetPlayerRequest
            {
                Id = id
            };

            var response = SendRequest<GetPlayerRequest, GetPlayerResponse>(RequestType.GetPlayer, request);

            return response.Player;
        }

        public static List<Player> GetAllPlayers()
        {
            var request = new GetAllPlayersRequest();

            var response =
                SendRequest<GetAllPlayersRequest, GetAllPlayersResponse>(RequestType.GetAllPlayers, request);

            return response.Players;
        }

        public static List<FocusPointItem> GetPlayerFocusPointItems(int memberId)
        {
            var request = new GetPlayerFocusPointsRequest
            {
                MemberId = memberId
            };

            var response =
                SendRequest<GetPlayerFocusPointsRequest, GetPlayerFocusPointsResponse>(
                    RequestType.GetPlayerFocusPoints, request);

            var result = response.FocusPoints.Select(p => new FocusPointItem {Descriptor = p}).ToList();

            return result;
        }

        public static Member GetMember(int id)
        {
            var request = new GetMemberRequest { Id = id};

            var response = SendRequest<GetMemberRequest, GetMemberResponse>(RequestType.GetMember, request);

            return response.Member;
        }

        public static Member GetLoggedInMember()
        {
            var request = new GetTokenMemberRequest();

            var response =
                SendRequest<GetTokenMemberRequest, GetTokenMemberResponse>(RequestType.GetTokenMember,
                    request);

            return response.Member;
        }


        public static List<PracticeTeam> GetPlayerPracticeTeams(Player player)
        {
            var request = new GetPlayerPracticeTeamRequest
            {
                Member = player.Member
            };

            var response =
                SendRequest<GetPlayerPracticeTeamRequest, GetPlayerPracticeTeamResponse>(
                    RequestType.GetMemberPracticeTeams, request);

            return response.PracticeTeams;
        }

        public static (List<PlaySession> playSessions, List<bool> relevance) GetSchedule(DateTime start, DateTime end)
        {
            var request = new GetScheduleRequest()
            {
                StartDate = start,
                EndDate = end
            };

            var response = SendRequest<GetScheduleRequest, GetScheduleResponse>(RequestType.GetSchedule, request);

            return (response.PlaySessions, response.IsRelevantForMember);
        }

        public static (List<Member> members, List<PracticeTeam> practiceTeams, List<FocusPointDescriptor> focusPoints)
            GetAdminPage()
        {
            var request = new GetAdminPageRequest();

            var response = SendRequest<GetAdminPageRequest, GetAdminPageResponse>(RequestType.GetAdminPage, request);

            return (response.Members, response.PracticeTeams, response.FocusPoints);
        }


        public static List<PracticeTeam> GetAllPracticeTeams()
        {
            var request = new GetAllPracticeTeamsRequest();

            var response =
                SendRequest<GetAllPracticeTeamsRequest, GetAllPracticeTeamsResponse>(RequestType.GetAllPracticeTeams,
                    request);

            return response.PracticeTeams;
        }

        public static FocusPointDescriptor GetFocusPointDescriptor(int id)
        {
            var request = new GetFocusPointDescriptorRequest
            {
                Id = id
            };

            var response =
                SendRequest<GetFocusPointDescriptorRequest, GetFocusPointDescriptorResponse>(
                    RequestType.GetFocusPointDescriptor, request);

            return response.FocusPointDescriptor;
        }

        public static PracticeTeam GetPracticeTeam(int id)
        {
            var request = new GetPracticeTeamRequest
            {
                Id = id,
            };

            var response = SendRequest<GetPracticeTeamRequest, GetPracticeTeamResponse>(RequestType.GetPracticeTeam, request);

            return response.Team;
        }

        public static List<PracticeTeam> GetTrainerPracticeTeams(Trainer trainer)
        {
            var request = new GetTrainerPracticeTeamsRequest
            {
                Trainer = trainer
            };

            var response =
                SendRequest<GetTrainerPracticeTeamsRequest, GetTrainerPracticeTeamsResponse>(
                    RequestType.GetTrainerPracticeTeams, request);

            return response.PracticeTeams;

        }

        // Setters below
        public static void SetPlayer(Player player)
        {
            var request = new SetPlayerRequest
            {
                Player = player
            };

            SendRequest<SetPlayerRequest, SetPlayerResponse>(RequestType.SetPlayer, request);
        }

        public static bool SetPlayerFocusPoints(Player player, FocusPointDescriptor focusPointDescriptor)
        {
            var request = new AddPlayerFocusPointRequest
            {
                Player = player,
                FocusPointDescriptor = focusPointDescriptor
            };

            var response =
                SendRequest<AddPlayerFocusPointRequest, AddPlayerFocusPointResponse>(
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

            var response = SendRequest<SetCommentRequest, SetCommentResponse>(RequestType.SetComment, request);
        }

        public static void SetPlayerPracticeTeams(Player player, PracticeTeam practiceTeam)
        {
            var request = new SetPlayerPracticeTeamRequest
            {
                Player = player,
                PracticeTeam = practiceTeam
            };

            SendRequest<SetPlayerPracticeTeamRequest, SetPlayerPracticeTeamResponse>(
                RequestType.SetPlayerPracticeTeam, request);
        }

        public static void ChangeTrainerPrivileges(Member member)
        {
            var request = new ChangeTrainerPrivilegesRequest
            {
                Member = member
            };

            SendRequest<ChangeTrainerPrivilegesRequest, ChangeTrainerPrivilegesResponse>(
                RequestType.ChangeTrainerPrivileges, request);
        }

        public static void SetNonPrivateFocusPoint(FocusPointDescriptor focusPointDescriptor)
        {
            var request = new SetNonPrivateFocusPointRequest
            {
                FocusPointDescriptor = focusPointDescriptor
            };

            SendRequest<SetNonPrivateFocusPointRequest, SetNonPrivateFocusPointResponse>(
                RequestType.SetNonPrivateFocusPoint, request);
        }

        public static void SetPracticeTeamTrainer(PracticeTeam practiceTeam, Trainer trainer)
        {
            var request = new SetPracticeTeamTrainerRequest
            {
                PracticeTeam = practiceTeam,
                Trainer = trainer,
            };

            SendRequest<SetPracticeTeamTrainerRequest, SetPracticeTeamTrainerResponse>(
                RequestType.SetPracticeTeamTrainer, request);
        }

        // Deleters below

        public static void DeletePlayerFocusPoints(Player player, FocusPointItem focusPointItem)
        {
            var request = new DeletePlayerFocusPointRequest
            {
                Player = player,
                FocusPointItem = focusPointItem
            };

            SendRequest<DeletePlayerFocusPointRequest, DeletePlayerFocusPointResponse>(
                RequestType.DeletePlayerFocusPoint, request);
        }
        public static void DeletePlayerPracticeTeam(Player player, PracticeTeam practiceTeam)
        {
            var request = new DeletePlayerPracticeTeamRequest
            {
                Player = player,
                PracticeTeam = practiceTeam
            };

            SendRequest<DeletePlayerPracticeTeamRequest, DeletePlayerPracticeTeamResponse>(
                RequestType.DeletePlayerPracticeTeam, request);
        }

        public static void DeleteFocusPointDescriptor(FocusPointDescriptor fp)
        {
            var request = new DeleteFocusPointDescriptorRequest
            {
                FocusPointDescriptor = fp
            };

            SendRequest<DeleteFocusPointDescriptorRequest, DeleteFocusPointDescriptorResponse>(
                RequestType.DeleteFocusPointDescriptor, request);
        }

        public static void DeletePracticeTeam(PracticeTeam team)
        {
            var request = new DeletePracticeTeamRequest
            {
                PracticeTeam = team
            };

            SendRequest<DeletePracticeTeamRequest, DeletePracticeTeamResponse>(
                RequestType.DeletePracticeTeam, request);
        }
        // creators below
        public static FocusPointDescriptor CreateFocusPointDescriptor(FocusPointDescriptor fp)
        {
            var request = new CreateFocusPointDescriptorRequest
            {
                FocusPointDescriptor = fp
            };

            var response =
                SendRequest<CreateFocusPointDescriptorRequest, CreateFocusPointDescriptorResponse>(
                    RequestType.CreateFocusPointDescriptor, request);

            return response.FocusPointDescriptor;
        }
        public static void SetExerciseDiscriptor(ExerciseDescriptor exercise)
        {
            var request = new SetExerciseDescriptorRequest
            {
                Exercise = exercise
            };

            var response = SendRequest<SetExerciseDescriptorRequest, SetExerciseDescriptorResponse>(RequestType.SetExerciseDiscriptor, request);
        }
        public static List<Trainer> GetAllTrainers()
        {
            var request = new GetAllTrainersRequest();

            var response =
                SendRequest<GetAllTrainersRequest, GetAllTrainersResponse>(RequestType.GetAllTrainers, request);

            return response.Trainers;
        }

        public static List<Member> GetAllMembers()
        {
            var request = new GetAllMembersRequest();
            var response =
                SendRequest<GetAllMembersRequest, GetAllMembersResponse>(RequestType.GetAllMembers, request);

            return response.Members;
        }

        public static List<RuleBreak> VerifyLineup(TeamMatch match)
        {
            var request = new VerifyLineupRequest() {Match = match};
            var response = SendRequest<VerifyLineupRequest, VerifyLineupResponse>(RequestType.VerifyLineup, request);
            return response.RuleBreaks;
        }

        public static void SetPracticeSession(PracticeSession practice)
        {
            var request = new SetPracticeSessionRequest
            {
                Practice = practice
            };

            var response =
                SendRequest<SetPracticeSessionRequest, SetPracticeSessionResponse>(
                    RequestType.SetPracticeSession, request);
        }

        public static void SetTeamMatch(TeamMatch match)
        {
            var request = new SetTeamMatchRequest()
            {
                TeamMatch = match
            };

            var response = SendRequest<SetTeamMatchRequest, SetTeamMatchResponse>(RequestType.SetTeamMatch, request);
        }

        public static void SetFeedback(Feedback feedback)
        {
            var request = new SetFeedbackRequest
            {
                Feedback = feedback
            };

            var response =
                SendRequest<SetFeedbackRequest, SetFeedbackResponse>(
                    RequestType.SetFeedback, request);
        }

        public static void SetPracticeTeam(PracticeTeam practiceTeam)
        {
            var request = new SetPracticeTeamRequest
            {
                PracticeTeam = practiceTeam
            };

            SendRequest<SetPracticeTeamRequest, SetPracticeTeamResponse>(RequestType.SetPracticeTeam, request);
        }


        public static void SetMemberSex(Sex newSex, Player player)
        {
            var request = new SetMemberSexRequest()
            {
                Player = player,
                NewSex = newSex
            };

            var response = SendRequest<SetMemberSexRequest, SetMemberSexResponse>(RequestType.SetMemberSex, request);
        }

        public static void DeleteTeamMatch(int id)
        {
            var request = new DeleteTeamMatchRequest
            {
                Id = id,
            };

            SendRequest<DeleteTeamMatchRequest, DeleteTeamMatchResponse>(RequestType.DeleteTeamMatch, request);
        }

        public static void DeletePracticeSession(int id)
        {
            var request = new DeletePracticeSessionRequest
            {
                Id = id,
            };

            SendRequest<DeletePracticeSessionRequest, DeletePracticeSessionResponse>(
                RequestType.DeletePracticeSession, request);
        }

        public static void EditFocusPoint(FocusPointDescriptor fp)
        {
            var request = new EditFocusPointRequest
            {
                FP = fp,
            };

            SendRequest<EditFocusPointRequest, EditFocusPointResponse>(RequestType.EditFocusPoint, request);
        }
    }
}
