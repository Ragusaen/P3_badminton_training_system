using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetTeamMatchHandler : MiddleRequestHandler<GetTeamMatchRequest, GetTeamMatchResponse>
    {
        protected override GetTeamMatchResponse InnerHandle(GetTeamMatchRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetTeamMatchResponse
            {
                TeamMatch = (TeamMatch) db.teammatches.Find(request.PlaySessionId)
            };
        }
    }
}
