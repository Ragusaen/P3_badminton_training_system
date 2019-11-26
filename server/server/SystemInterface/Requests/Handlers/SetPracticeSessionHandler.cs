using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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
                member = db.members.Find(e.Trainer.Member.Id),
                practiceteam = db.practiceteams.Find(e.PracticeTeam.Id),
                practicesessionexercises = e.Exercises.Select(p => new practicesessionexercise { exercise = db.exercises.Find(p.ExerciseDescriptor.Id), Minutes = p.Minutes, ExerciseIndex = p.Index }).ToList(),
                focuspoint = db.focuspoints.Find(e.MainFocusPoint.Descriptor.Id),
                focuspoints = e.FocusPoints.Select(p => db.focuspoints.Find(p.Descriptor.Id)).ToList(),
            };

            var d = db.practicesessions.Add(dbPS);

            db.SaveChanges();
            return new SetPracticeSessionResponse();
        }
    }
}
