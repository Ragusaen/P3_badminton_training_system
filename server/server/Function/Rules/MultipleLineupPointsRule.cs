using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MultipleLineupsPointsRule : IRule
    {
        public int Priority { get; set; }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();
            List<Lineup> lineups = new List<Lineup>(); //All lineups above current one
            foreach (Lineup lineup in lineups)
                ruleBreaks.AddRange(CompareLineups(match.Lineup, lineup));

            return ruleBreaks;
        }

        private List<RuleBreak> CompareLineups(Lineup lineup, Lineup lineupAbove)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();

            throw new NotImplementedException();
        }
    }
}
