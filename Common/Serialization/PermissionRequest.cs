using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [Serializable]
    public class PermissionRequest : Request
    {
        public byte[] Token;
    }

    [Serializable]
    public class PermissionResponse : Response
    {
        public bool AccessDenied = false;
    }
}
