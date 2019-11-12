using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeTeam
    {
        public string Name;
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
