using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace server.Model
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
        public List<RuleBreak> RuleBakes { get; set; } = new List<RuleBreak>();
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}

