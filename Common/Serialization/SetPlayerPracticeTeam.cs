using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPlayerPracticeTeamRequest : PermissionRequest
    {
        public Player Player;
        public PracticeTeam PracticeTeam;
    }

    [Serializable, XmlRoot]
    public class SetPlayerPracticeTeamResponse : PermissionResponse
    { }
}
