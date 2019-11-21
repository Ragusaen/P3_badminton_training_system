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
        private static Logger _log = LogManager.GetCurrentClassLogger();

        protected override SetPlayerPracticeTeamsResponse InnerHandle(SetPlayerPracticeTeamsRequest request, member requester)
        {
            if (requester.MemberType == (int) MemberType.Trainer || request.Player.Member.Id != requester.ID)
                return null;
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
