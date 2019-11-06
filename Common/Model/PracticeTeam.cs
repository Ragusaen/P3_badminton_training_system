using System.Collections.Generic;

namespace Common.Model
{
    class PracticeTeam
    {


        public List<Player> Players { get; set; } = new List<Player>();
        public List<PracticeSession> Practices { get; set; } = new List<PracticeSession>();
    }
}
