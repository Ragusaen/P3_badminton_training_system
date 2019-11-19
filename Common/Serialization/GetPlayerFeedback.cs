using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlayerFeedbackRequest : PermissionRequest
    {
        public int MemberId;
    }

    [Serializable, XmlRoot]
    public class GetPlayerFeedbackResponse : PermissionResponse
    {
        public List<Feedback> Feedback;
    }
}
