using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetPracticeSessionFocusPointsHandler : MiddleRequestHandler<GetPracticeSessionFocusPointsRequest, GetPracticeSessionFocusPointsResponse>
    {
        protected override GetPracticeSessionFocusPointsResponse InnerHandle(GetPracticeSessionFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPracticeSessionFocusPointsResponse
            {
                FocusPoints = db.practicesessions.
                    Single(p => p.PlaySessionID == request.PlaySessionId).focuspoints.
                    Select(p => (Common.Model.FocusPointDescriptor) p).
                    ToList()
            };
        }
    }
}
