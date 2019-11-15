using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class GetPlayerFeedbackRequest : PermissionRequest
    {
        public int MemberId;
    }

    public class GetPlayerFeedbackResponse : PermissionResponse
    {
        public List<Feedback> Feedback;
    }
}
