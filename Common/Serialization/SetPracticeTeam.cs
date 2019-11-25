using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class SetPracticeTeamRequest : PermissionRequest
    {
        public PracticeTeam PracticeTeam;
    }

    [XmlRoot]
    public class SetPracticeTeamResponse : PermissionResponse
    { }
}
