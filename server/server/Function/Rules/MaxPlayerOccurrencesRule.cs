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
            var playersGroup = GetPlayersInLineup(match.Lineup);

            //Find all players that appears more than _max times.
            var tooManyOccurrencesIds = playersGroup.players.GroupBy(p => p.Member.Id)
                .Where(g => g.Count() > _max)
                .Select(y => y.Key)
                .ToList();

            //Add rulebreaks to players.
            AddRulebreaksToIllegalPlayers(tooManyOccurrencesIds, match.Lineup, $"Player can not appear more than { _max} times on a lineup!");

            //If lineup does not contain a reserve,
            //get ids of players that appear less than _max times
            //and add rulebreaks.
            if (!playersGroup.reserveOnLineup)
            {
                var tooFewOccurrencesIds = playersGroup.players.GroupBy(p => p.Member.Id)
                    .Where(g => g.Count() < _max)
                    .Select(y => y.Key)
                    .ToList();
                AddRulebreaksToIllegalPlayers(tooFewOccurrencesIds, match.Lineup, $"Player must appear { _max} times on a lineup!");
            }
            return _ruleBreaks;
        }

        //Get all players in a lineup.
        //Also returns if the lineup contains a reserve.
        private (List<Player> players, bool reserveOnLineup) GetPlayersInLineup(Lineup lineup)
        {
            var players = new List<Player>();
            bool reserveOnLineup = false;

            foreach (var group in lineup)
            {
                foreach (var position in group.Positions)
                {
                    if (position.Player != null)
                    {
                        players.Add(position.Player);
                        if (position.IsExtra) reserveOnLineup = true;
                    }
                    if (Lineup.PositionType.Double.HasFlag(group.Type) && position.OtherPlayer != null)
                    {
                        players.Add(position.OtherPlayer);
                        if (position.OtherIsExtra) reserveOnLineup = true;
                    }
                }
            }
            return (players, reserveOnLineup);
        }

        //Add rulebreaks to players with certain ids on the lineup.
        private void AddRulebreaksToIllegalPlayers(List<int> ids, Lineup lineup, string error)
        {
            foreach (var group in lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if (pos.Player != null && ids.Contains(pos.Player.Member.Id))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, error));
                    
                    if (pos.OtherPlayer != null && Lineup.PositionType.Double.HasFlag(group.Type) && ids.Contains(pos.OtherPlayer.Member.Id))
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, error));
                }
            }
        }
    }
}