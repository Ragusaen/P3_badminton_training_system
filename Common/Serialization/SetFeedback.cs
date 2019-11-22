using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetFeedbackRequest : PermissionRequest
    {
        public Feedback Feedback;
    }

    [Serializable, XmlRoot]
    public class SetFeedbackResponse : PermissionResponse
    { }
}
