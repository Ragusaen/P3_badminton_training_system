using System;
using System.Collections.Generic;

namespace Common.Model
{
    public class Feedback
    {
        public Player Player { get; set; }
        public PlaySession PlaySession { get; set; }
        public int? ReadyQuestion { get; set; }
        public int? EffortQuestion { get; set; }
        public int? ChallengeQuestion { get; set; }
        public int? AbsorbQuestion { get; set; }
        public string GoodQuestion { get; set; }
        public string BadQuestion { get; set; }
        public string FocusPointQuestion { get; set; }
        public string DayQuestion { get; set; }
    }
}
