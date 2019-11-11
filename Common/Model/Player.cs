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
        public enum AgeGroup 
        {
            U9,
            U11,
            U13,
            U15,
            U17,
            Senior,
            Veteran
        }

        public List<PracticeTeam> PracticeTeams;
        public PlayerRanking Rankings;

        public AgeGroup Age;
        public Sex Sex;
        public int BadmintonPlayerId;
    }
}
