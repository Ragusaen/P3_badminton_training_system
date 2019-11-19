using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetAllFocusPointsRequest : Request
    {
        
    }

    [Serializable, XmlRoot]
    public class GetAllFocusPointsResponse : Response
    {
        [DataMember] public List<FocusPointDescriptor> FocusPointDescriptors;
    }
}
