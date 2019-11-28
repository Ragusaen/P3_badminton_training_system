using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.Function.Rules
{
    class SamePositionType : IRule
    {
        public int Priority { get; set; } = 5;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            Dictionary<int, int> playerCount;

            foreach (var group in match.Lineup)
            {
                playerCount = new Dictionary<int, int>();
                foreach (var pos in group.Positions)
                {
                    if (pos.Player != null)
                    {
                        if(!playerCount.ContainsKey(pos.Player.Member.Id))
                            playerCount.Add(pos.Player.Member.Id, 0);
                        playerCount[pos.Player.Member.Id]++;
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null)
                    {
                        if (!playerCount.ContainsKey(pos.OtherPlayer.Member.Id))
                            playerCount.Add(pos.OtherPlayer.Member.Id, 0);
                        playerCount[pos.OtherPlayer.Member.Id]++;
                    }
                }
                playerCount.Where(p => p.Value > 1).ToList().ForEach(p => AddRuleBreaks(p.Key, group));
            }
            return _ruleBreaks;
        }

        private void AddRuleBreaks(int illegalPlayerId, Lineup.Group group)
        {
            for (int i = 0; i < group.Positions.Count; i++)
            {
                var pos = group.Positions[i];
                if(illegalPlayerId == pos.Player.Member.Id)
                    _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Player can not play twice in same position type!"));
                if(Lineup.PositionType.Double.HasFlag(group.Type) && illegalPlayerId == pos.OtherPlayer.Member.Id)
                    _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Player can not play twice in same position type!"));
            }
        }
    }
}
