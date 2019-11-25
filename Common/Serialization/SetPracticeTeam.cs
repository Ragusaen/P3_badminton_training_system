using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPracticeTeamRequest : PermissionRequest
    {
        public PracticeTeam Team;
    }

    [Serializable, XmlRoot]
    public class SetPracticeTeamResponse : PermissionResponse
    { }
}
