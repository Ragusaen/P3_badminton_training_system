using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlaySessionFeedbackRequest : Request
    {
        public int PlaySessionId;
    }

    [Serializable, XmlRoot]
    public class GetPlaySessionFeedbackResponse : Response
    {
        public List<Feedback> Feedback;
    }
}