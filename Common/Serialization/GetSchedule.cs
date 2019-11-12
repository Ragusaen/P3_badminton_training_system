using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [DataContract]
    class GetScheduleRequest
    {
        [DataMember] public DateTime StartData;
        [DataMember] public DateTime EndDate;
    }

    [DataContract]
    class GetScheduleResponse
    {
    }
}
