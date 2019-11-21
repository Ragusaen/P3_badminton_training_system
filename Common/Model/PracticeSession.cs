using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeSession : PlaySession
    {
        public PracticeTeam PracticeTeam { get; set; }
        public Trainer Trainer { get; set; }
        public List<ExerciseItem> Exercises { get; set; }

    }
}
