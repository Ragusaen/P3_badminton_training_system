
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
        //Setters below
        SetPlayer,
        SetPlayerFocusPoints,
        SetComment,
        //Deleters below
        DeletePlayerFocusPoints,
        GetExercises,
        //Creators below
        CreateFocusPointDescriptor,
    };
}