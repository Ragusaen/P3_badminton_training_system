using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetAllMembersRequest : Request
    { }

    [Serializable, XmlRoot]
    public class GetAllMembersResponse : Response
    {
        public List<Member> Members;
    }
}
