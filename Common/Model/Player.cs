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
        public PlayerRanking Rankings;

        public Sex Sex;
        public int BadmintonPlayerId;
    }
}
