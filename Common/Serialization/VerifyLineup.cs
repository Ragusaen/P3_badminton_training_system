using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class VerifyLineupRequest : Request
    {
        public TeamMatch Match;
    }

    [Serializable, XmlRoot]
    public class VerifyLineupResponse : Response
    {
        public List<RuleBreak> RuleBreaks;
    }
}
