using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Serialization
{
    public class PermissionRequest : Request
    {
        public byte[] Token;
    }

    public class PermissionResponse : Response
    {
        public bool AccessDenied = true;
    }
}
