using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
