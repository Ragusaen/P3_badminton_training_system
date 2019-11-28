using Common.Model;

namespace Server.DAL
{
    partial class feedback
    {
        public static explicit operator Feedback(feedback f)
        {
            var db = new DatabaseEntities();
            PlaySession ps;

            // Create the correct derived type for the playsession
            if ((PlaySession.Type) f.playsession.Type == PlaySession.Type.Practice)
                ps = (PracticeSession) db.practicesessions.Find(f.PlaySessionID);
            else
                ps = (TeamMatch) db.teammatches.Find(f.PlaySessionID);

            return new Feedback
            {
                PlaySession = ps,
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