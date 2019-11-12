using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeTeam
    {

        public string Name { get; set; }
        public int Id { get; set; }
        public List<Member> Members { get; set; }
        //public List<Player> Players { get; set; } = new List<Player>();
        //public List<PracticeSession> Practices { get; set; } = new List<PracticeSession>();
    }
}
