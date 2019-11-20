using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlayerFocusPointsRequest : Request
    {
        public int MemberId;
    }

    [Serializable, XmlRoot]
    public class GetPlayerFocusPointsResponse : Response
    {
        public List<FocusPointDescriptor> FocusPoints;
    }
}
