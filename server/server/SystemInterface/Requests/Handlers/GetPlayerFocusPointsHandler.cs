using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetPlayerFocusPointsHandler :MiddleRequestHandler<GetPlayerFocusPointsRequest, GetPlayerFocusPointsResponse>
    {
        protected override GetPlayerFocusPointsResponse InnerHandle(GetPlayerFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPlayerFocusPointsResponse
            {
                FocusPoints = db.members
                    .Single(p => p.ID == request.MemberId).focuspoints
                    .Select(p => (Common.Model.FocusPointDescriptor) p)
                    .ToList()
            };
        }
    }
}
