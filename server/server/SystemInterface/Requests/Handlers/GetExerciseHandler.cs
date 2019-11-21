using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;
using Common.Model;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetExerciseHandler : MiddleRequestHandler<GetExercisesRequest, GetExercisesResponse>
    {
        protected override GetExercisesResponse InnerHandle(GetExercisesRequest request, member requester)
        {
            var db = new DatabaseEntities();
            return new GetExercisesResponse() { Exercises = db.exercises.ToList().Select(p => (ExerciseDescriptor)p).ToList()};
        }
    }
}
