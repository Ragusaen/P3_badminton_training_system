using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    class RuleBreak
    {
        public Player Player { get; set; }
        public string Brake { get; set; }

        public RuleBreak(Player player, string brake)
        {
            Player = player;
            Brake = brake;
        }
    }
}
