using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetMemberPracticeTeamRequest : Request
    {
        public Member Member;
    }

    [XmlRoot]
    public class GetMemberPracticeTeamResponse : Response
    {
        public List<PracticeTeam> PracticeTeams;
    }
}
