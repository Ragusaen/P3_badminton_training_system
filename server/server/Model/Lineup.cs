using Server.Model;
using System.Collections.Generic;


namespace Server.Model
{
    class Lineup
    {
        internal enum Leagues
        {
            BatmintonLeague,
            Devision1,
            Devision2,
            Devision3,
            DenmarkSeries,
            RegionalSeries,
            Series1,
            Series2,
            Series3,
            Series4
        }
        public Match Match { get; set; }
        public Leagues League { get; set; }
        public int Round { get; set; }
        public List<RuleBreak> RuleBakes { get; set; } = new List<RuleBreak>();
        public List<IPosition> Positions { get; set; } = new List<IPosition>();
    }
}

