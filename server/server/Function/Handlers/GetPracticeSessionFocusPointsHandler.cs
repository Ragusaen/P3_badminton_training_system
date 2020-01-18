using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPracticeSessionFocusPointsHandler : MiddleRequestHandler<GetPracticeSessionFocusPointsRequest, GetPracticeSessionFocusPointsResponse>
    {
        protected override GetPracticeSessionFocusPointsResponse InnerHandle(GetPracticeSessionFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPracticeSessionFocusPointsResponse
            {
                FocusPoints = db.practicesessions.
                    Single(p => p.PlaySessionID == request.PlaySessionId).subfocuspoints.
                    Select(p => (Common.Model.FocusPointDescriptor) p).
                    ToList()
            };
        }
    }
}
