using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetTokenMemberRequest : PermissionRequest
    { }

    [DataContract]
    public class GetTokenMemberResponse : PermissionResponse
    {
        public Member Member;
    }
}
