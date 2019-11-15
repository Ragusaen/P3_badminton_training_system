using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class Feedback
    {
        public Player Player { get; set; }
        public PlaySession PlaySession { get; set; }
        public int? ReadyQuestion;
        public int? EffortQuestion;
        public int? ChallengeQuestion;
        public int? AbsorbQuestion;
        public string GoodQuestion;
        public string BadQuestion;
        public string FocusPointQuestion;
        public string DayQuestion;
    }
}
