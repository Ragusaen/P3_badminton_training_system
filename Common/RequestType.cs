﻿
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
        GetPlayerPracticeTeams,
        GetPlayerFocusPoints,
        GetTeamMatch,
        GetTeamMatchPositions,
        GetPracticeSessionFocusPoints,
        GetPracticeSessionExercises,
        GetPracticeTeamYearPlan,
        GetPlayer,
        // Setters below
        SetPlayer,
        SetPlayerFocusPoints,
    };
}