using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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