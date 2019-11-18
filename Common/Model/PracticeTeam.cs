using System.Collections.Generic;

namespace Common.Model
{
    public class PracticeTeam
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public List<YearPlanSection> YearPlan { get; set; }
    }
}
