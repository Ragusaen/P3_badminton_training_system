using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetExerciseDescriptorHandler : MiddleRequestHandler<SetExerciseDescriptorRequest, SetExerciseDescriptorResponse>
    {
        protected override SetExerciseDescriptorResponse InnerHandle(SetExerciseDescriptorRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new SetExerciseDescriptorResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var e = request.Exercise;
            var dbEx = new exercise
            {
                Name = e.Name,
                Description = e.Description
            };

            db.exercises.Add(dbEx);
            db.SaveChanges();
            return new SetExerciseDescriptorResponse();
        }
    }
}
