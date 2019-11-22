
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
        GetAdminPage,
        GetAllPracticeTeams,
        //Setters below
        SetPlayer,
        SetPlayerFocusPoints,
        SetComment,
        SetPlayerPracticeTeams,
        ChangeTrainerPrivileges,
        //Deleters below
        DeletePlayerFocusPoint,
        DeletePlayerPracticeTeam,
        GetExercises,
        //Creators below
        CreateFocusPointDescriptor,
        SetExerciseDiscriptor,
        GetAllTrainers,
        SetPracticeSession,
    };
}