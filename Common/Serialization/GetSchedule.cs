using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetScheduleRequest : Request
    {
        public DateTime StartDate;
        public DateTime EndDate;
    }

    [XmlRoot]
    public class GetScheduleResponse : Response
    {
        public List<PlaySession> PlaySessions;
        public List<bool> IsRelevantForMember;
    }
}
