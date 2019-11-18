using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetTeamMatchHandler : MiddleRequestHandler<GetTeamMatchRequest, GetTeamMatchResponse>
    {
        protected override GetTeamMatchResponse InnerHandle(GetTeamMatchRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetTeamMatchResponse
            {
                TeamMatches = (Common.Model.PlaySession) db.playsessions.Single(p => p.ID == request.PlaySessionId)
            };
        }
    }
}
