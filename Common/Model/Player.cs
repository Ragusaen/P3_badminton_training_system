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
        public PlayerRanking Rankings { get; set; }
        public Sex Sex { get; set; }
        public int BadmintonPlayerId { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public List<FocusPointItem> FocusPointItems;
    }
}
