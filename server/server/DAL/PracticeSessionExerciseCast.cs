using Common.Model;

namespace server.DAL
{
    partial class practicesessionexercise
    {
        public static explicit operator Common.Model.ExerciseItem(practicesessionexercise pse)
        {
            return new ExerciseItem
            {
                Id = pse.ExerciseID,
                ExerciseDescriptor = (Common.Model.ExerciseDescriptor) pse.exercise,
                Index = pse.ExerciseIndex,
                Minutes = pse.Minutes
            };
        }
    }
}
