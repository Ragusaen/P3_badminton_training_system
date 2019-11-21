using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class SetExerciseDescriptorHandler : MiddleRequestHandler<SetExerciseDescriptorRequest, SetExerciseDescriptorResponse>
    {
        protected override SetExerciseDescriptorResponse InnerHandle(SetExerciseDescriptorRequest request, member requester)
        {
            if ((requester.MemberType == (int)MemberType.Trainer))
                return null;
            var db = new DatabaseEntities();
            var e = request.Exercise;
            var dbEx = new exercise
            {
                Name = e.Name,
                Description = e.Description
            };

            db.exercises.Add(dbEx);
            db.SaveChanges();
            return null;
            
        }
    }
}
