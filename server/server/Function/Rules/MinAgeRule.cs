using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MinAgeRule : IRule
    {
        public int Priority { get; set; }
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
                for (int i = 0; i < group.positions.Count; i++)
                {
                    if (CheckAge(group.positions[i].Player))
                        _ruleBreaks.Add(new RuleBreak((group.type, i), 0, $"Player is too young; must be at least {_minAge.ToString()}"));

                    if(Lineup.PositionType.Double.HasFlag(group.type) && CheckAge(group.positions[i].OtherPlayer))
                        _ruleBreaks.Add(new RuleBreak((group.type, i), 1, $"Player is too young; must be at least {_minAge.ToString()}"));
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