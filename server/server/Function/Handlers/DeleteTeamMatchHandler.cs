using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class DeleteTeamMatchHandler : MiddleRequestHandler<DeleteTeamMatchRequest, DeleteTeamMatchResponse>
    {
        protected override DeleteTeamMatchResponse InnerHandle(DeleteTeamMatchRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeleteTeamMatchResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var match = db.teammatches.Find(request.Id);
            if (match != null)
            {
                var positions = match.positions.ToList();
                var captain = match.captain;
                var ps = match.playsession;
                captain?.teammatches.Remove(match);
                db.positions.RemoveRange(positions);
                db.teammatches.Remove(match);
                if (ps != null)
                {
                    var fb = ps.feedbacks;
                    db.feedbacks.RemoveRange(fb);
                    db.playsessions.Remove(ps);
                }
            }

            db.SaveChanges();
            return new DeleteTeamMatchResponse();
        }
    }
}
