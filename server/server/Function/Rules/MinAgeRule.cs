using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MinAgeRule : IRule
    {
        public int Priority { get; set; } = 4;
        private PlayerRanking.AgeGroup _minAge;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public MinAgeRule(PlayerRanking.AgeGroup minAge)
        {
            _minAge = minAge;
        }


        public List<RuleBreak> Rule(TeamMatch match)
        {
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    if (group.Positions[i].Player != null && CheckAge(group.Positions[i].Player))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Player is too young; must be at least {_minAge.ToString()}"));

                    if(Lineup.PositionType.Double.HasFlag(group.Type) && group.Positions[i].OtherPlayer != null && CheckAge(group.Positions[i].OtherPlayer))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Player is too young; must be at least {_minAge.ToString()}"));
                }
            }
                
            return _ruleBreaks;
        }

        private bool CheckAge(Player p)
        {
            return p.Rankings.Age >= _minAge;
        }
    }
}