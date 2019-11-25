using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class playsession
    {
        public static explicit operator Common.Model.TeamMatch(playsession p)
        {
            var tm = p.teammatch;
            return new TeamMatch()
            {
                Captain = (Member)tm.member,
                End = p.EndDate,
                League = (TeamMatch.Leagues)tm.League,
                LeagueRound = tm.LeagueRound,
                Lineup = (new LineUpCast()).CreateLineup(tm.positions),
                Location = p.Location,
                OpponentName = tm.OpponentName,
                Season = tm.Season,
                Start = p.StartDate
            };
        }

        public static explicit operator Common.Model.PracticeSession(playsession p)
        {
            var db = new DatabaseEntities();
            var ps = db.practicesessions.Find(p.ID);
            return new PracticeSession
            {
                Id = ps.PlaySessionID,
                Location = p.Location,
                Start = p.StartDate,
                End = p.EndDate,
                Trainer = (Common.Model.Trainer)db.members.Find(ps.TrainerID),
                FocusPoints = ps.focuspoints.ToList().Select(fp => new FocusPointItem() { Descriptor = (FocusPointDescriptor)fp }).ToList(),
                Exercises = ps.practicesessionexercises.ToList().Select(e => (ExerciseItem)e).ToList(),
                MainFocusPoint = new FocusPointItem { Descriptor = (FocusPointDescriptor)ps.focuspoint },
                PracticeTeam = (PracticeTeam)ps.practiceteam,
            };
        }
    }

}
