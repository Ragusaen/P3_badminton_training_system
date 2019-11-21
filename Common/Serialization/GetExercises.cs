using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Serialization
{
    public class GetExercisesRequest : Request
    {
    }
    public class GetExercisesResponse : Response 
    {
        public List<ExerciseDescriptor> Exercises;
    }
}
