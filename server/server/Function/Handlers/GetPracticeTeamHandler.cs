using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetPracticeTeamHandler : MiddleRequestHandler<GetPracticeTeamRequest, GetPracticeTeamResponse>
    {
        protected override GetPracticeTeamResponse InnerHandle(GetPracticeTeamRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var dbPracticeTeam = db.practiceteams.Find(request.Id);
            var practiceTeam = (PracticeTeam)dbPracticeTeam;
            practiceTeam.Players = dbPracticeTeam.players.ToList().Select(p => (Common.Model.Player) p).ToList();

            var response = new GetPracticeTeamResponse
            {
                Team = practiceTeam
            };

            _log.Debug($"New practice team created: {practiceTeam.Name}");

            return response;
        }
    }
}
