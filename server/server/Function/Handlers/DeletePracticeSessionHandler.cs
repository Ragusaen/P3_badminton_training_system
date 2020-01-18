using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class DeletePracticeSessionHandler : MiddleRequestHandler<DeletePracticeSessionRequest, DeletePracticeSessionResponse>
    {
        protected override DeletePracticeSessionResponse InnerHandle(DeletePracticeSessionRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeletePracticeSessionResponse { AccessDenied = true };
            }
            var db = new DatabaseEntities();

            var prac = db.practicesessions.Find(request.Id);

            if (prac != null)
            {
                var captain = prac.trainer;
                var team = prac.practiceteam;
                var ps = db.playsessions.Find(prac.PlaySessionID);
                captain?.practicesessions.Remove(prac);
                team.practicesessions.Remove(prac);

                db.practicesessionexercises.RemoveRange(prac.practicesessionexercises);
                prac.subfocuspoints.Clear();
                prac.mainfocuspoint = null;

                if (ps != null)
                {
                    db.feedbacks.RemoveRange(ps.feedbacks);

                    db.playsessions.Remove(ps);
                }
            }

            db.SaveChanges();
            return new DeletePracticeSessionResponse();
        }
    }
}
