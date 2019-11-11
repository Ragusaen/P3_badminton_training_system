using System.Collections.Generic;

namespace Common.Model
{
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
        public int Sex;
        public int BadmintonPlayerId;
    }
}
