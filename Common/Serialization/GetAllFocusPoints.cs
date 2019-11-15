using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetAllFocusPointsRequest : Request
    {
        
    }

    [DataContract]
    public class GetAllFocusPointsResponse : Response
    {
        [DataMember] public List<FocusPointDescriptor> FocusPointDescriptors;
    }
}
