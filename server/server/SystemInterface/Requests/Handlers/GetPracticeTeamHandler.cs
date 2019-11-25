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
    class GetPracticeTeamHandler : MiddleRequestHandler<GetPracticeTeamRequest, GetPracticeTeamResponse>
    {
        protected override GetPracticeTeamResponse InnerHandle(GetPracticeTeamRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var dbPracticeTeam = db.practiceteams.Find(request.Id);
            var practiceTeam = (PracticeTeam)dbPracticeTeam;
            practiceTeam.Players = dbPracticeTeam.members.ToList().Select(p => (Common.Model.Player) p).ToList();

            var response = new GetPracticeTeamResponse
            {
                Team = practiceTeam
            };

            _log.Debug($"New practice team created: {practiceTeam.Name}");

            return response;
        }
    }
}
