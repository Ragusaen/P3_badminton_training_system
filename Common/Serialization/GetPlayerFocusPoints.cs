using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPlayerFocusPointsRequest : Request
    {
        [DataMember] public int MemberId;
    }

    [DataContract]
    public class GetPlayerFocusPointsResponse : Response
    {
        [DataMember] public List<FocusPointDescriptor> FocusPoints;
    }
}
