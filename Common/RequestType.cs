﻿
namespace Common
{
    public enum RequestType {
        ConnectionTest,
        Login,
        CreateAccount,
        VerifyLineup,
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
        DeleteFocusPointDescriptor,
        DeletePracticeTeam,
        GetExercises,
        //Creators below
        CreateFocusPointDescriptor,
        SetExerciseDiscriptor,
        GetAllTrainers,
        SetPracticeSession,
        SetFeedback,
        SetPracticeTeam,
    };
}