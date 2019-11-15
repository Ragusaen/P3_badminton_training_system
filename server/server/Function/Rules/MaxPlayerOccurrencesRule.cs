using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class MaxPlayerOccurrencesRule : IRule
    {
        public int Priority { get; set; }
        private int _max;

        public MaxPlayerOccurrencesRule(int maxOccurrences)
        {
            _max = maxOccurrences;
        }

        public List<RuleBreak> Rule(TeamMatch match) //Clean up
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();
            int count, count2 = 0;
            foreach (var pos in match.Lineup.Positions)
            {
                count = count2 = 0;
                foreach (var pos2 in match.Lineup.Positions)
                {
                    if (pos.Value.Player.Member.Id == pos2.Value.Player.Member.Id) count++;
                    if (pos2.Value.OtherPlayer != null)
                    {
                        var dp2 = pos2.Value.OtherPlayer;
                        if (pos.Value.Player.Member.Id == dp2.Member.Id) count++;
                        if (pos.Value.OtherPlayer != null && pos.Value.OtherPlayer.Member.Id == dp2.Member.Id) count2++;
                    }
                    else
                    {
                        if (pos.Value.OtherPlayer != null && pos.Value.OtherPlayer.Member.Id == pos2.Value.Player.Member.Id) count2++;
                    }
                }

                if (count > _max)
                    ruleBreaks.Add(new RuleBreak(pos.Key, 0, $"Player can only play on {_max} positions!"));
                if (count2 > _max)
                    ruleBreaks.Add(new RuleBreak(pos.Key, 1, $"Player can only play on {_max} positions!"));
            }
            return ruleBreaks;
        }
    }
}