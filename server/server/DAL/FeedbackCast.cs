using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class feedback
    {
        public static explicit operator Common.Model.Feedback(Server.DAL.feedback f)
        {
            return new Feedback
            {
                ReadyQuestion = f.Ready,
                EffortQuestion = f.Effort,
                ChallengeQuestion = f.Challenge,
                AbsorbQuestion = f.Absorb,
                GoodQuestion = f.Good,
                BadQuestion = f.Bad,
                FocusPointQuestion = f.FocusPoint,
                DayQuestion = f.Day
            };
        }
    }
}
