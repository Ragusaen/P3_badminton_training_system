
namespace Common
{
    public enum RequestType {
        ConnectionTest,
        Login,
        CreateAccount,
        GetSchedule,
        GetPracticeTeam,
        GetPlayersWithNoAccount,
        GetAllPlayers,
        GetPlayerFeedback,
        GetPlaySessionFeedback,
        GetPracticeSession,
        GetAllFocusPoints,
        GetMemberPracticeTeams,
        GetPlayerFocusPoints,
        GetTeamMatch,
        GetTeamMatchPositions,
        GetPracticeSessionFocusPoints,
        GetPracticeSessionExercises,
        GetPracticeTeamYearPlan,
        GetPlayer,
        GetTokenMember,
        GetAllPracticeTeams,
        //Setters below
        SetPlayer,
        SetPlayerFocusPoints,
        SetComment,
        SetPlayerPracticeTeams,
        //Deleters below
        DeletePlayerFocusPoints,
        //Creators below
        CreateFocusPointDescriptor,
    };
}