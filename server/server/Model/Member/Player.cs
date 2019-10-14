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
        public List<PracticeTeam> Teams { get; set; }

        public string BadmintonId { get; private set; }

        public PlayerRanking RankingLevel;
        public PlayerRanking RankingSingle;
        public PlayerRanking RankingDouble;
        public PlayerRanking RankingMixed;

        public Player(Member member, string badmintonId) : base(member)
        {
            BadmintonId = badmintonId;
        }
    }
}
