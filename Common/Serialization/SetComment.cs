using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class SetCommentRequest : PermissionRequest
    {
        public Member Member;
        public string NewComment;
    }

    public class SetCommentResponse : PermissionResponse
    {

    }
}
