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
    class SetPlayerPracticeTeamsHandler : MiddleRequestHandler<SetPlayerPracticeTeamsRequest, SetPlayerPracticeTeamsResponse>
    {
        protected override SetPlayerPracticeTeamsResponse InnerHandle(SetPlayerPracticeTeamsRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                RequestMember = request.Player.Member;
                return new SetPlayerPracticeTeamsResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var dbPlayer = db.members.Find(request.Player.Member.Id);
            var teams = request.PracticeTeams;

            foreach (var pt in teams)
            {
                var dbPt = db.practiceteams.Find(pt.Id);

                if (dbPlayer.practiceteams.SingleOrDefault(p => p.ID == pt.Id) == null)
                {
                    dbPlayer.practiceteams.Add(dbPt);
                    _log.Debug($"Player: {dbPlayer.Name} received new team: {pt.Name}");
                }
            }

            db.SaveChanges();
            return new SetPlayerPracticeTeamsResponse();
        }
    }
}
