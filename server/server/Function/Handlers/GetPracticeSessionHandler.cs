using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPracticeSessionHandler : MiddleRequestHandler<GetPracticeSessionRequest, GetPracticeSessionResponse>
    {
        protected override GetPracticeSessionResponse InnerHandle(GetPracticeSessionRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetPracticeSessionResponse
            {
                PracticeSession = (Common.Model.PracticeSession) db.practicesessions.First(p => p.PlaySessionID == request.Id)
            };
        }
    }
}