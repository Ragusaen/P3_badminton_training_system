﻿using System.Collections.Generic;
using Common.Model.Positions;

namespace Common.Model
{
    public class Lineup
    {
        public enum Leagues
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

