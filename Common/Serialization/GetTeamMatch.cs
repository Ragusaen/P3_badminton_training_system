using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetTeamMatchRequest : Request
    {
        public int PlaySessionId;
    }

    [XmlRoot]
    public class GetTeamMatchResponse : Response
    {
        public TeamMatch TeamMatch;
    }
}
