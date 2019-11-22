using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPracticeSessionRequest : PermissionRequest
    {
        public PracticeSession Practice;
    }

    [Serializable, XmlRoot]
    public class SetPracticeSessionResponse : PermissionResponse
    { }
}
