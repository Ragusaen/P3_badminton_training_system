using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class practicesessionexercise
    {
        public static explicit operator Common.Model.ExerciseItem(Server.DAL.practicesessionexercise pse)
        {
            return new ExerciseItem
            {
                Id = pse.ExerciseID,
                ExerciseDescriptor = (Common.Model.ExerciseDescriptor) pse.exercise,
                Index = pse.ExerciseIndex,
            };
        }
    }
}
