using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPracticeTeamHandler : MiddleRequestHandler<GetPracticeTeamRequest, GetPracticeTeamResponse>
    {
        protected override GetPracticeTeamResponse InnerHandle(GetPracticeTeamRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var dbPracticeTeam = db.practiceteams.Find(request.Id);
            var practiceTeam = (PracticeTeam)dbPracticeTeam;
            List<Player> players = dbPracticeTeam.players.ToList().Select(p => (Common.Model.Player) p).ToList();

            var response = new GetPracticeTeamResponse
            {
                Team = practiceTeam,
                Players = players
            };

            _log.Debug($"New practice team created: {practiceTeam.Name}");

            return response;
        }
    }
}
