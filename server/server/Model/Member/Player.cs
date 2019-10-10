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

        public int BadmintonId;
        public PlayerRanking Rankings;

        public Player(Member member) : base(member)
        {

        }
    }
}
