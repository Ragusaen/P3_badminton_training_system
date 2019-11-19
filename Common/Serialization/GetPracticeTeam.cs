using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPracticeTeamRequest : Request
    {
        public int Id;
    }

    [Serializable, XmlRoot]
    public class GetPracticeTeamResponse : Response
    {
        public PracticeTeam Team;
    }
}
