using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MinAgeRule : IRule
    {
        public int Priority { get; set; }
        private Player.AgeGroup _minAge;

        public MinAgeRule(Player.AgeGroup minAge)
        {
            _minAge = minAge;
        }

        public List<RuleBreak> Rule(Match match)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();
            foreach (var position in match.Lineup.Positions)
            {
                if (position.Value.Player.Age < _minAge)
                    ruleBreaks.Add(new RuleBreak(position.Key, 0, "Player too young"));
                if (position.Value is DoublePosition dp && dp.OtherPlayer.Age < _minAge)
                    ruleBreaks.Add(new RuleBreak(position.Key, 1, "Player too young"));
            }
            return ruleBreaks;
        }
    }
}