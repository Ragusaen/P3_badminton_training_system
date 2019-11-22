using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]

    public class ChangeTrainerPrivilegesRequest : PermissionRequest
    {
        public Member Member;
    }
    [Serializable, XmlRoot]
    public class ChangeTrainerPrivilegesResponse : PermissionResponse
    {
    }
}
