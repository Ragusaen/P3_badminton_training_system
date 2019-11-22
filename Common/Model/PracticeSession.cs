using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeSession : PlaySession
    {
        public PracticeTeam PracticeTeam { get; set; }
        public Trainer Trainer { get; set; }
        public List<ExerciseItem> Exercises { get; set; }
        public List<FocusPointItem> FocusPoints { get; set; }
        public FocusPointItem MainFocusPoint { get; set; }

    }
}
