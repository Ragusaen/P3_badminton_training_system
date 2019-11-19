using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlayerPracticeTeamRequest : Request
    {
        public int MemberId;
    }

    [Serializable, XmlRoot]
    public class GetPlayerPracticeTeamResponse : Response
    {
        public List<PracticeTeam> PracticeTeams;
    }
}
