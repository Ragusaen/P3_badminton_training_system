using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class ExerciseItem
    {
        public int Id { get; set; }
        public ExerciseDescriptor ExerciseDescriptor { get; set; }
        public int Index { get; set; }
        public int Minutes { get; set; }
    }
}
