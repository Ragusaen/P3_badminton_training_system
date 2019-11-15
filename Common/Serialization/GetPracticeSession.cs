using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeSessionRequest
    {
        [DataMember] public int Id;
    }

    [DataContract]
    public class GetPracticeSessionResponse
    {
        [DataMember] public PracticeSession PracticeSession;
    }
}