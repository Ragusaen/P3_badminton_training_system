using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Trainer Trainer { get; set; }
    }
}
