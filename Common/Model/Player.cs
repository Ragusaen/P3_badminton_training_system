using System.Collections.Generic;

namespace Common.Model
{
    // Uses ISO/IEC 5218
    public enum Sex
    {
        Unknown,
        Male,
        Female,
        NotApplicable = 9
    }

    public class Player : MemberRole
    {
        public Sex Sex { get; set; }
        public int BadmintonPlayerId { get; set; }
        public bool OnRankList { get; set; }

        public PlayerRanking Rankings { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<FocusPointItem> FocusPointItems { get; set; }
        public List<PracticeTeam> PracticeTeams { get; set; }
    }
}
