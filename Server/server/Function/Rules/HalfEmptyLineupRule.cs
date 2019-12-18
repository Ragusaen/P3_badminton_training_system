using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;

namespace Server.Function.Rules
{
    class HalfEmptyLineupRule : IRule
    {
        public int Priority { get; set; } = 6;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Verify(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            int posCount = match.Lineup.Sum(p => p.Positions.Count);
            int nullPosCount = match.Lineup.Sum(p => p.Positions.Count(q => CheckPositionNull(q, p.Type)));
            if (nullPosCount > posCount / 2)
                AddRuleBreaks(match);

            return _ruleBreaks;
        }

        private bool CheckPositionNull(Position pos, Lineup.PositionType type)
        {
            if (pos.Player == null)
                return true;
            if (Lineup.PositionType.Double.HasFlag(type) && pos.OtherPlayer == null)
                return true;
            return false;
        }

        private void AddRuleBreaks(TeamMatch match)
        {
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if(pos.Player == null)
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Lineup must be at least half full!"));
                    if(Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer == null)
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Lineup must be at least half full!"));
                }
            }
        }
    }
}
