using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MultipleLineupsPointsRule : IRule
    {
        public int Priority { get; set; }
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            List<Lineup> lineups = new List<Lineup>(); //TODO: Get all lineups above current one
            
            foreach (Lineup lineup in lineups)
                _ruleBreaks.AddRange(CompareLineups(match.Lineup, lineup));

            return _ruleBreaks;
        }

        private List<RuleBreak> CompareLineups(Lineup lineup, Lineup lineupAbove)
        {
            throw new NotImplementedException();
        }
    }
}
