
namespace Common
{
    /// <summary>
    /// A enum of all types of request
    /// </summary>
    public enum RequestType {
        Login,
        CreateAccount,
        VerifyLineup,
        GetSchedule,
        GetPracticeTeam,
        GetPlayersWithNoAccount,
        GetAllPlayers,
        GetAllTrainers,
        GetAllMembers,
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
        GetPlayer,
        GetTokenMember,
        GetAdminPage,
        GetAllPracticeTeams,
        GetMember,
        GetFocusPointDescriptor,
        GetTrainerPracticeTeams,
        //Setters below
        SetPlayer,
        SetPlayerFocusPoints,
        SetComment,
        SetPlayerPracticeTeam,
        ChangeTrainerPrivileges,
        SetNonPrivateFocusPoint,
        SetPracticeTeamTrainer,
        SetMemberSex,
        EditFocusPoint,
        //Deleters below
        DeletePlayerFocusPoint,
        DeletePlayerPracticeTeam,
        DeleteFocusPointDescriptor,
        DeletePracticeTeam,
        GetExercises,
        DeleteTeamMatch,
        DeletePracticeSession,
        //Creators below
        CreateFocusPointDescriptor,
        SetExerciseDiscriptor,
        SetPracticeSession,
        SetTeamMatch,
        SetFeedback,
        SetPracticeTeam,
    };
}