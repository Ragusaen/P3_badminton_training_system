using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class teammatch
    {
        public static explicit operator Common.Model.TeamMatch(Server.DAL.teammatch tm)
        {
            var db = new DatabaseEntities();
            return new TeamMatch()
            {
                Id = tm.PlaySessionID,
                Captain = (Member)db.members.First(p => p.ID == tm.CaptainID),
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
