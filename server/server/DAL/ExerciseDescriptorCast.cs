using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class exercise
    {
        public static explicit operator Common.Model.ExerciseDescriptor(Server.DAL.exercise e)
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
