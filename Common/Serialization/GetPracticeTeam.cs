using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeTeamRequest : Request
    {
        [DataMember] public int Id;
    }

    [DataContract]
    public class GetPracticeTeamResponse : Response
    {
        [DataMember] public PracticeTeam Team;
    }
}
