using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetTeamMatchPositionsHandler : MiddleRequestHandler<GetTeamMatchPositionsRequest, GetTeamMatchPositionsResponse>
    {
        protected override GetTeamMatchPositionsResponse InnerHandle(GetTeamMatchPositionsRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetTeamMatchPositionsResponse
            {
                Positions =  db.teammatches.Single(p => p.PlaySessionID == request.PlaySessionId).positions.Select(p => (Common.Model.Position) p).ToList()
            };
        }
    }
}
