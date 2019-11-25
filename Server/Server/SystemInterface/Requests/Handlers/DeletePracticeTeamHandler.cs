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
    class DeletePracticeTeamHandler : MiddleRequestHandler<DeletePracticeTeamRequest, DeletePracticeTeamResponse>
    {
        protected override DeletePracticeTeamResponse InnerHandle(DeletePracticeTeamRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeletePracticeTeamResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var team = db.practiceteams.Find(request.PracticeTeam.Id);
            
            if (team != null)
            {
                var members = team.members.ToList();
                var practiceSessions = team.practicesessions.ToList();

                foreach (var m in members)
                {
                    m.practiceteams.Remove(team);
                }

                foreach (var ps in practiceSessions)
                {
                    ps.practiceteam = null;
                }
                db.practiceteams.Remove(team);
                _log.Debug($"Completely removed practice team {team.Name}");
            }

            db.SaveChanges();
            return new DeletePracticeTeamResponse();
        }
    }
}
