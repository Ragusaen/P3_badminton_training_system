using Common.Model;

namespace server.DAL
{
    partial class teammatch
    {
        public static explicit operator Common.Model.TeamMatch(teammatch tm)
        {
            var db = new DatabaseEntities();
            return new TeamMatch()
            {
                Id = tm.PlaySessionID,
                Captain = tm.captain == null ? null : (Member)tm.captain,
                End = tm.playsession.EndDate,
                Start = tm.playsession.StartDate,
                League = (TeamMatch.Leagues)tm.League,
                LeagueRound = tm.LeagueRound,
                Season = tm.Season,
                TeamIndex = tm.TeamIndex,
                Location = tm.playsession.Location,
                OpponentName = tm.OpponentName,
                Lineup = new LineUpCast().CreateLineup(tm.positions)
            };
        }
    }
}
