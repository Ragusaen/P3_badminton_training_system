using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Serialization
{
    public class BindAccountToMemberRequest : PermissionRequest
    {
        public string Username;
        public bool IsPlayer;
        public int? BadmintonPlayerId;
        public string NameOverWrite;
    }

    public class BindAccountToMemberResponse : PermissionResponse
    {
        public bool WasSuccessful;
    }
}
