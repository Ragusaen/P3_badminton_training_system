using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeSessionFocusPointsRequest : Request
    {
        [DataMember] public int PlaySessionId;
    }

    [DataContract]
    public class GetPracticeSessionFocusPointsResponse : Response
    {
        [DataMember] public List<FocusPointDescriptor> FocusPoints;
    }
}
