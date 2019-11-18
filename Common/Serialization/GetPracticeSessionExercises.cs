using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPracticeSessionExercisesRequest : Request
    {
        [DataMember] public int PlaySessionId;
    }

    [DataContract]
    public class GetPracticeSessionExercisesResponse : Response
    {
        [DataMember] public List<ExerciseItem> Exercises;
    }
}
