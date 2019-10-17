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
            U9,
            U11,
            U13,
            U15,
            U17,
            Senior,
            Veteran
        } 
        public List<PracticeTeam> Teams { get; set; }

        public PlayerRanking Rankings = new PlayerRanking();
        public AgeGroup Age { get; set; }
        public int BadmintonPlayerId { get; private set; }

        public Player(Member member, int badmintonPlayerId) : base(member)
        {
            BadmintonPlayerId = badmintonPlayerId;
        }

        public override string ToString()
        {
            return base.ToString() + " - " + BadmintonPlayerId;
        }
    }
}
