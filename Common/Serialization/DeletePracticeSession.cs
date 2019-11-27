using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [XmlRoot]
    public class DeletePracticeSessionRequest : PermissionRequest
    {
        public int Id;
    }

    [XmlRoot]
    public class DeletePracticeSessionResponse : PermissionResponse
    { }
}
