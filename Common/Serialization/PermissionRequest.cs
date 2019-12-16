using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [Serializable]
    public abstract class PermissionRequest : Request
    {
        public byte[] Token;
    }

    [Serializable]
    public abstract class PermissionResponse : Response
    {
        public bool AccessDenied = false;
    }
}
