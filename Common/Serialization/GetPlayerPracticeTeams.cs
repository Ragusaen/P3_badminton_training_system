using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPlayerPracticeTeamRequest : Request
    {
        [DataMember] public int MemberId;
    }

    [DataContract]
    public class GetPlayerPracticeTeamResponse : Response
    {
        [DataMember] public List<PracticeTeam> PracticeTeams;
    }
}
