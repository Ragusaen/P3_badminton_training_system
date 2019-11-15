using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetTeamMatchRequest : Request
    {
        [DataMember] public int PlaySessionId;
    }

    [DataContract]
    public class GetTeamMatchResponse : Response
    {
        [DataMember] public PlaySession TeamMatches;
    }
}
