using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Server.Function.Rules
{
    class MaxPlayerOccurrencesRule : IRule
    {
        public int Priority { get; set; } = 9;
        private int _max;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public MaxPlayerOccurrencesRule(int maxOccurrences)
        {
            _max = maxOccurrences;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            List<Player> players = GetPlayersInLineup(match.Lineup);

            //Find all players that appears more than _max times
            var ids = players.GroupBy(p => p.Member.Id)
                .Where(g => g.Count() > _max)
                .Select(y => y.Key)
                .ToList();

            AddRulebreaksToIllegalPlayers(ids, match.Lineup);
            return _ruleBreaks;
        }

        private List<Player> GetPlayersInLineup(Lineup lineup)
        {
            List<Player> players = new List<Player>();

            foreach (var group in lineup)
            {
                foreach (var position in group.Positions)
                {
                    if(position.Player != null)
                        players.Add(position.Player);
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && position.OtherPlayer != null)
                        players.Add(position.OtherPlayer);
                }
            }

            return players;
        }

        private void AddRulebreaksToIllegalPlayers(List<int> ids, Lineup lineup)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if (pos.Player != null && ids.Contains(pos.Player.Member.Id))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Player can not appear more than {_max} times on a lineup!"));
                    
                    if (pos.OtherPlayer != null && Lineup.PositionType.Double.HasFlag(group.Type) && ids.Contains(pos.OtherPlayer.Member.Id))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Player can not appear more than {_max} times on a lineup!"));
                }
            }
        }
    }
}