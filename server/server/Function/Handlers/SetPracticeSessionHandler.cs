using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetPracticeSessionHandler : MiddleRequestHandler<SetPracticeSessionRequest, SetPracticeSessionResponse>
    {
        protected override SetPracticeSessionResponse InnerHandle(SetPracticeSessionRequest request, member requester)
        {
            if (!((MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
                return new SetPracticeSessionResponse() { AccessDenied = true };

            var db = new DatabaseEntities();
            var e = request.Practice;

            var playsession = new playsession
            {
                EndDate = e.End,
                StartDate = e.Start,
                Location = e.Location,
                Type = (int) PlaySession.Type.Practice
            };

            playsession = db.playsessions.Add(playsession);

            var dbPS = new practicesession
            {
                playsession = playsession,
                trainer = e.Trainer == null ? null : db.members.Find(e.Trainer.Member.Id),
                practiceteam = db.practiceteams.Find(e.PracticeTeam.Id),
                practicesessionexercises = e.Exercises.Select(p => new practicesessionexercise { exercise = db.exercises.Find(p.ExerciseDescriptor.Id), Minutes = p.Minutes, ExerciseIndex = p.Index }).ToList(),
                mainfocuspoint = e.MainFocusPoint == null ? null : db.focuspoints.Find(e.MainFocusPoint.Descriptor.Id),
                subfocuspoints = e.FocusPoints.Select(p => db.focuspoints.Find(p.Descriptor.Id)).ToList(),
            };
            db.practicesessions.Add(dbPS);

            db.SaveChanges();
            return new SetPracticeSessionResponse();
        }
    }
}
