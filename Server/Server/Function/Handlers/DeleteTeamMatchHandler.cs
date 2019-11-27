using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;
using Server.SystemInterface.Requests.Handlers;

namespace Server.Function.Handlers
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
                db.positions.RemoveRange(positions);
                match.member = null;
                db.teammatches.Remove(match);
                db.playsessions.Remove(db.playsessions.Find(request.Id));
            }

            db.SaveChanges();
            return new DeleteTeamMatchResponse();
        }
    }
}
