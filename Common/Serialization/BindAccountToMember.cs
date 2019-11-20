using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class BindAccountToMemberRequest : PermissionRequest
    {
        public string Username;
        public bool IsPlayer;
        public int? BadmintonPlayerId;
        public string NameOverWrite;
    }

    [Serializable, XmlRoot]
    public class BindAccountToMemberResponse : PermissionResponse
    {
        public bool WasSuccessful;
    }
}
