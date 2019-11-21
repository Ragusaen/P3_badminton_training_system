using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Function.Rules
{
    class MaxPlayerOccurrencesRule : IRule
    {
        public int Priority { get; set; }
        private int _max;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public MaxPlayerOccurrencesRule(int maxOccurrences)
        {
            _max = maxOccurrences;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            List<Player> players = GetPlayersInLineup(match.Lineup);

            //Find all players that appears more than _max times in lineup
            players = players.GroupBy(p => p)
                .Where(p => p.Count() > _max)
                .Select(y => y.Key)
                .ToList();

            AddRulebreaksToIllegalPlayers(players, match.Lineup);
            return _ruleBreaks;
        }

        private List<Player> GetPlayersInLineup(Lineup lineup)
        {
            List<Player> players = new List<Player>();

            foreach (var group in lineup)
            {
                foreach (var position in group.positions)
                {
                    players.Add(position.Player);
                    if (Lineup.PositionType.Double.HasFlag(group.type))
                        players.Add(position.OtherPlayer);
                }
            }

            return players;
        }

        private void AddRulebreaksToIllegalPlayers(List<Player> players, Lineup lineup)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.positions.Count; i++)
                {
                    if (players.Contains(group.positions[i].Player))
                        _ruleBreaks.Add(new RuleBreak((group.type, i), 0, $"Player can not appear more than {_max} times on a lineup!"));
                    if (Lineup.PositionType.Double.HasFlag(group.type) && players.Contains(group.positions[i].OtherPlayer))
                        _ruleBreaks.Add(new RuleBreak((group.type, i), 1, $"Player can not appear more than {_max} times on a lineup!"));
                }
            }
        }
    }
}