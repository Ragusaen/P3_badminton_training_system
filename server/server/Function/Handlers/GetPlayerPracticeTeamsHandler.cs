using System.Linq;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetPlayerPracticeTeamsHandler : MiddleRequestHandler<GetPlayerPracticeTeamRequest, GetPlayerPracticeTeamResponse>
    {
        protected override GetPlayerPracticeTeamResponse InnerHandle(GetPlayerPracticeTeamRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPlayerPracticeTeamResponse
            {
                PracticeTeams = db.members.Single(p => p.ID == request.Member.Id).practiceteamsplayer
                    .Select(p => (Common.Model.PracticeTeam) p).ToList()
            };
        }
    }
}
