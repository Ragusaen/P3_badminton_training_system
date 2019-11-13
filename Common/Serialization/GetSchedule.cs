using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetScheduleRequest
    {
        [DataMember] public DateTime StartData;
        [DataMember] public DateTime EndDate;
    }

    [DataContract]
    public class GetScheduleResponse
    {
        [DataMember] public List<PracticeSession> PracticeSessions;
        [DataMember] public List<Match> Matches;
    }
}
