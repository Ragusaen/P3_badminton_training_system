using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetTeamMatchPositionsRequest : Request
    {
        [DataMember] public int PlaySessionId;
    }

    [DataContract]
    public class GetTeamMatchPositionsResponse : Response
    {
        [DataMember] public List<Position> Positions;
    }
}
