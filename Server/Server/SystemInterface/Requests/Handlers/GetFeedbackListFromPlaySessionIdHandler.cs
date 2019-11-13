﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetFeedbackListFromPlaySessionIdHandler : MiddleRequestHandler<GetFeedbackRequest, GetFeedbackResponse>
    {
        protected override GetFeedbackResponse InnerHandle(GetFeedbackRequest request)
        {
            var db = new DatabaseEntities();
            var response = new GetFeedbackResponse { FeedbackList = new List<Feedback>() };

            response.FeedbackList = db.feedbacks.Where(p => p.PlaySessionID == request.Id).Select(p => (Common.Model.Feedback)p).ToList();

            return response;
        }
    }
}
