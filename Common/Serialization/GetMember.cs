using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetMemberRequest : Request
    {
        public int Id;
    }

    [XmlRoot]
    public class GetMemberResponse : Response
    {
        public Member Member;
    }
}
