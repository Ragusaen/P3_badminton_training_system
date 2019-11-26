using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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
