using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Serialization
{
    public class PermissionRequest
    {
        public byte[] Token;
    }

    public class PermissionResponse
    {
        public bool AccessDenied;
    }
}
