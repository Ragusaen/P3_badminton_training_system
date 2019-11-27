using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class practicesession
    {
        public static explicit operator Common.Model.PracticeSession(Server.DAL.practicesession ps)
        {
            var db = new DatabaseEntities();
            return new PracticeSession
            {
                Id = ps.PlaySessionID,
                Location = ps.playsession.Location,
                Start = ps.playsession.StartDate,
                End = ps.playsession.EndDate,
                Trainer = (Trainer)db.members.Find(ps.TrainerID),
                FocusPoints = ps.subfocuspoints.ToList().Select(fp => new FocusPointItem() {Descriptor = (FocusPointDescriptor)fp}).ToList(),
                Exercises = ps.practicesessionexercises.ToList().Select(e => (ExerciseItem)e).ToList(),
                MainFocusPoint = ps.MainFocusPointID == null ? null : new FocusPointItem {Descriptor = (FocusPointDescriptor)ps.mainfocuspoint},
                PracticeTeam = ps.practiceteam == null ? null : (PracticeTeam)ps.practiceteam,
            };
        }
    }
}
