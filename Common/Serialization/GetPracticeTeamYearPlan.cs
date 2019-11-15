using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeTeamYearPlanRequest : Request
    {
        [DataMember] public int PracticeTeamId;
    }

    [DataContract]
    public class GetPracticeTeamYearPlanResponse : Response
    {
        [DataMember] public List<YearPlanSection> YearPlan;
    }
}
