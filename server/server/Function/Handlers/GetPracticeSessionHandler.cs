using System.Linq;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
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