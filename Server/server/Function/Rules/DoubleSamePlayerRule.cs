using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;

namespace Server.Function.Rules
{
    class DoubleSamePlayerRule : IRule
    {
        public int Priority { get; set; } = 3;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];

                    //If position is double and contains the same player, add rulebreaks.
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.Player != null && pos.OtherPlayer != null &&
                        pos.Player.Member.Id == group.Positions[i].OtherPlayer.Member.Id)
                    {
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Double can not consist of the same player"));
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Double can not consist of the same player"));
                    }
                }
            }

            return _ruleBreaks;
        }
    }
}
