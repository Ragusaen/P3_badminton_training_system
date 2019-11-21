using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPlayerPracticeTeamsRequest : PermissionRequest
    {
        public Player Player;
        public List<PracticeTeam> PracticeTeams;
    }

    [Serializable, XmlRoot]
     public class SetPlayerPracticeTeamsResponse : PermissionResponse
    {

    }
}
