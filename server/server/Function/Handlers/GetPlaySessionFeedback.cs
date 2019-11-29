using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetPlaySessionFeedback : MiddleRequestHandler<GetPlaySessionFeedbackRequest, GetPlaySessionFeedbackResponse>
    {
        protected override GetPlaySessionFeedbackResponse InnerHandle(GetPlaySessionFeedbackRequest request, member member)
        {
            var db = new DatabaseEntities();
            var response = new GetPlaySessionFeedbackResponse { Feedback = new List<Feedback>() };

            response.Feedback = db.feedbacks.Where(p => p.PlaySessionID == request.PlaySessionId).Select(p => (Common.Model.Feedback)p).ToList();

            return response;
        }
    }
}
