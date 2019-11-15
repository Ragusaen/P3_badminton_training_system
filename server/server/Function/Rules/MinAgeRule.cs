using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MinAgeRule : IRule
    {
        public int Priority { get; set; }
        private PlayerRanking.AgeGroup _minAge;

        public MinAgeRule(PlayerRanking.AgeGroup minAge)
        {
            _minAge = minAge;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();
            foreach (var position in match.Lineup.Positions)
            {
                if (position.Value.Player.Rankings.Age < _minAge)
                    ruleBreaks.Add(new RuleBreak(position.Key, 0, "Player too young"));
                if (position.Value.OtherPlayer != null && position.Value.OtherPlayer.Rankings.Age < _minAge)
                    ruleBreaks.Add(new RuleBreak(position.Key, 1, "Player too young"));
            }
            return ruleBreaks;
        }
    }
}