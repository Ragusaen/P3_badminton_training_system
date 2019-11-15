using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeTeamRequest
    {
        [DataMember] public int Id;
    }

    [DataContract]
    public class GetPracticeTeamResponse
    {
        [DataMember] public PracticeTeam Team;
    }
}
