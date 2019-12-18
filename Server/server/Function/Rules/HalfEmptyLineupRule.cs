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
        public int Priority { get; set; } = 2;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            //Gets amount of positions
            int posCount = match.Lineup.Sum(p => p.Positions.Count);

            //Gets amount of positions that are empty
            int nullPosCount = match.Lineup.Sum(p => p.Positions.Count(q => CheckPositionNull(q, p.Type)));

            //If half the lineup or more is empty, add rulebreaks.
            if (nullPosCount > posCount / 2)
                AddRuleBreaks(match);

            return _ruleBreaks;
        }

        //Checks if position is empty.
        private bool CheckPositionNull(Position pos, Lineup.PositionType type)
        {
            if (pos.Player == null)
                return true;
            if (Lineup.PositionType.Double.HasFlag(type) && pos.OtherPlayer == null)
                return true;
            return false;
        }

        //Add rulebreaks to empty positions.
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
