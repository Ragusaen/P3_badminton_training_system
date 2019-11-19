using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [DataContract]
    [KnownType(typeof(GetTokenMemberRequest))]
    public class PermissionRequest : Request
    {
        [DataMember] public byte[] Token;
    }

    [DataContract]
    public class PermissionResponse : Response
    {
        [DataMember] public bool AccessDenied = true;
    }
}
