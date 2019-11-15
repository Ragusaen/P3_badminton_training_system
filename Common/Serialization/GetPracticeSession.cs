using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeSessionRequest : Request
    {
        [DataMember] public int Id;
    }

    [DataContract]
    public class GetPracticeSessionResponse : Response
    {
        [DataMember] public PracticeSession PracticeSession;
    }
}