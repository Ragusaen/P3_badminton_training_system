using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPlaySessionFeedbackRequest
    {
        [DataMember] public int PlaySessionId;
    }

    [DataContract]
    public class GetPlaySessionFeedbackResponse
    {
        [DataMember] public List<Feedback> Feedback;
    }
}