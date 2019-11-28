using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class SetMemberSexRequest : PermissionRequest
    {
        public Player Player;
        public Sex NewSex;
    }

    public class SetMemberSexResponse : PermissionResponse
    {

    }
}
