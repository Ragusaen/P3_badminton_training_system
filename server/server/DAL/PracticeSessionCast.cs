using System.Linq;
using Common.Model;

namespace server.DAL
{
    partial class practicesession
    {
        public static explicit operator Common.Model.PracticeSession(practicesession ps)
        {
            var db = new DatabaseEntities();
            return new PracticeSession
            {
                Id = ps.PlaySessionID,
                Location = ps.playsession.Location,
                Start = ps.playsession.StartDate,
                End = ps.playsession.EndDate,
                Trainer = ps.trainer == null ? null : (Trainer)db.members.Find(ps.TrainerID),
                FocusPoints = ps.subfocuspoints.ToList().Select(fp => new FocusPointItem() {Descriptor = (FocusPointDescriptor)fp}).ToList(),
                Exercises = ps.practicesessionexercises.ToList().Select(e => (ExerciseItem)e).ToList(),
                MainFocusPoint = ps.MainFocusPointID == null ? null : new FocusPointItem {Descriptor = (FocusPointDescriptor)ps.mainfocuspoint},
                PracticeTeam = (PracticeTeam)ps.practiceteam,
            };
        }
    }
}
