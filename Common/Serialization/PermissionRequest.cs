using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [DataContract]
    public class PermissionRequest : Request
    {
        public byte[] Token;
    }

    [DataContract]
    public class PermissionResponse : Response
    {
        public bool AccessDenied = true;
    }
}
