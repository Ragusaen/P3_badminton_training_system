using System.Collections.Generic;

namespace Common.Model.Member
{
    class Player : MemberRole
    {
        internal enum AgeGroup 
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
        public int BadmintonPlayerId;
    }
}
