using Common.Model;

namespace server.DAL
{
    partial class exercise
    {
        public static explicit operator Common.Model.ExerciseDescriptor(exercise e)
        {
            return new ExerciseDescriptor
            {
                Id = e.ID,
                Name = e.Name,
                Description = e.Description
            };
        }
    }
}
