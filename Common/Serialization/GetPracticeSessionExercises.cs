using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPracticeSessionExercisesRequest : Request
    {
        public int PlaySessionId;
    }

    [Serializable, XmlRoot]
    public class GetPracticeSessionExercisesResponse : Response
    {
        public List<ExerciseItem> Exercises;
    }
}
