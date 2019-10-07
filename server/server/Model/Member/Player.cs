using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    public class Player : MemberRole
    {
        public List<PracticeTeam> Teams { get; set; }

        public string BadmintonId;

        public PlayerRanking RankingLevel;
        public PlayerRanking RankingSingle;
        public PlayerRanking RankingDouble;
        public PlayerRanking RankingMixed;

        public Player(Member member) : base(member)
        {

        }
    }
}
