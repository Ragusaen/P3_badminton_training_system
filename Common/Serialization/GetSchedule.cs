using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetScheduleRequest : Request
    {
        public DateTime StartDate;
        public DateTime EndDate;
    }

    [Serializable, XmlRoot]
    public class GetScheduleResponse : Response
    {
        public List<PracticeSession> PracticeSessions;
        public List<TeamMatch> Matches;
    }
}
