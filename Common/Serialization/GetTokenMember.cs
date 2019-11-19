using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetTokenMemberRequest : PermissionRequest
    { }

    [Serializable, XmlRoot]
    public class GetTokenMemberResponse : PermissionResponse
    {
        public Member Member;
    }
}
