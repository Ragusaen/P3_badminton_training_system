using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class GetTokenMemberRequest : PermissionRequest
    { }

    public class GetTokenMemberResponse : PermissionResponse
    {
        public Member Member;
    }
}
