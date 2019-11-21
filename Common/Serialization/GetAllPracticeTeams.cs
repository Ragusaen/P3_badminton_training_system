using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetAllPracticeTeamsRequest : Request
    { }
    [Serializable, XmlRoot]
    public class GetAllPracticeTeamsResponse : Response
    {
        public List<PracticeTeam> PracticeTeams;
    }
}
