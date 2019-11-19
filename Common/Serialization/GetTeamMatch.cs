using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetTeamMatchRequest : Request
    {
        public int PlaySessionId;
    }

    [Serializable, XmlRoot]
    public class GetTeamMatchResponse : Response
    {
        public PlaySession TeamMatches;
    }
}
