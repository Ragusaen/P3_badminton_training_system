using System.Collections.Generic;
using Common.Model.Member;

namespace Common.Model
{
    public class PracticeTeam
    {


        public List<Player> Players { get; set; } = new List<Player>();
        public List<PracticeSession> Practices { get; set; } = new List<PracticeSession>();
    }
}
