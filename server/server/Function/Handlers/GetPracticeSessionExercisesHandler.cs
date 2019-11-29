using System.Linq;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetPracticeSessionExercisesHandler :MiddleRequestHandler<GetPracticeSessionExercisesRequest, GetPracticeSessionExercisesResponse>
    {
        protected override GetPracticeSessionExercisesResponse InnerHandle(GetPracticeSessionExercisesRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPracticeSessionExercisesResponse
            {
                Exercises = db.practicesessions.
                    Single(p => p.PlaySessionID == request.PlaySessionId).practicesessionexercises.
                    Select(p => (Common.Model.ExerciseItem) p)
                    .ToList()
            };
        }
    }
}
