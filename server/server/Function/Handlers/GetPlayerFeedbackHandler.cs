using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPlayerFeedbackHandler : MiddleRequestHandler<GetPlayerFeedbackRequest, GetPlayerFeedbackResponse>
    {
        protected override GetPlayerFeedbackResponse InnerHandle(GetPlayerFeedbackRequest request, member requester)
        {
            if (!((MemberType)requester.MemberType).HasFlag(MemberType.Trainer) && requester.ID != request.MemberId)
                return new GetPlayerFeedbackResponse()
                {
                    AccessDenied = true
                };

            var db = new DatabaseEntities();
            var response = new GetPlayerFeedbackResponse { Feedback = new List<Feedback>() };

            response.Feedback = db.feedbacks.Where(p => p.MemberID == request.MemberId).ToList().Select(p => (Common.Model.Feedback)p).ToList();

            return response;
        }
    }
}
