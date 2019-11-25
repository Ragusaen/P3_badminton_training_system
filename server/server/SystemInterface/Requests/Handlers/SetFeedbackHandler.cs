using Common.Model;
using Common.Serialization;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.SystemInterface.Requests.Handlers
{
    class SetFeedbackHandler : MiddleRequestHandler<SetFeedbackRequest, SetFeedbackResponse>
    {
        protected override SetFeedbackResponse InnerHandle(SetFeedbackRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Player))
                return new SetFeedbackResponse() { AccessDenied = true};

            var db = new DatabaseEntities();
            var e = request.Feedback;
            var dbFB = new feedback
            {
                Absorb = e.AbsorbQuestion,
                Bad = e.BadQuestion,
                Challenge = e.ChallengeQuestion,
                Day = e.DayQuestion,
                Effort = e.EffortQuestion,
                Good = e.GoodQuestion,
                FocusPoint = e.FocusPointQuestion,
                Ready = e.ReadyQuestion,
                member = db.members.Find(requester.ID),
                playsession = db.playsessions.Find(e.PlaySession.Id)
            };
            db.feedbacks.Add(dbFB);
            db.SaveChanges();
            return new SetFeedbackResponse();

        }
    }
}
