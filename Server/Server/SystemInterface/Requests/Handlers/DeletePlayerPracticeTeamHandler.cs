using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class DeletePlayerPracticeTeamHandler : MiddleRequestHandler<DeletePlayerPracticeTeamRequest, DeletePlayerPracticeTeamResponse>
    {
        protected override DeletePlayerPracticeTeamResponse InnerHandle(DeletePlayerPracticeTeamRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                RequestMember = request.Player.Member;
                return new DeletePlayerPracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbPt = db.practiceteams.Find(request.PracticeTeam.Id);
            db.members.Find(request.Player.Member.Id).practiceteams.Remove(dbPt);

            _log.Debug($"Player: {request.Player.Member.Name} removed from Practice Team: {dbPt.Name}");

            return new DeletePlayerPracticeTeamResponse();
        }
    }
}
