using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class Player : MemberRole
    {
        public List<PracticeTeam> Teams { get; set; }

        public int BadmintonPlayerID;
        public PlayerRanking Rankings = new PlayerRanking();

        public Player(Member member, int badmintonPlayerID) : base(member)
        {
            BadmintonPlayerID = badmintonPlayerID;
        }   
    }
}
