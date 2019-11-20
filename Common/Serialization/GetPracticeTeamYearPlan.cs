using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPracticeTeamYearPlanRequest : Request
    {
        public int PracticeTeamId;
    }

    [Serializable, XmlRoot]
    public class GetPracticeTeamYearPlanResponse : Response
    {
        public List<YearPlanSection> YearPlan;
    }
}
