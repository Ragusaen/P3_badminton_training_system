using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Server.Model
{
    class Player : MemberRole
    {
        internal enum AgeGroup 
        {
            U15,
            U17,
            Senior
        } 
        public List<PracticeTeam> Teams { get; set; }

        public PlayerRanking Rankings = new PlayerRanking();
        public AgeGroup Age { get; set; }
        public int BadmintonPlayerId { get; private set; }

        public Player(Member member, int badmintonPlayerId) : base(member)
        {
            BadmintonPlayerId = badmintonPlayerId;
        }
    }
}
