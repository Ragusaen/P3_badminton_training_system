using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.DAL;

namespace Server.Function.Rules
{
    class MultipleLineupsPointsRule : IRule
    {
        public int Priority { get; set; } = 15;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            var db = new DatabaseEntities();
            List<TeamMatch> aboveMatches = db.teammatches
                .Where(m => m.Season == match.Season && m.LeagueRound == match.LeagueRound && m.TeamIndex < match.TeamIndex)
                .ToList()
                .Select(p => (TeamMatch)p)
                .ToList();
            
            foreach (TeamMatch teamMatch in aboveMatches)
                CompareLineups(match, teamMatch);

            return _ruleBreaks;
        }

        private void CompareLineups(TeamMatch match, TeamMatch matchAbove)
        {
            //If upper lineup is placed Division 1 or above, compare category. Else compare niveau.
            if (matchAbove.League > TeamMatch.Leagues.Division2)
                CompareLineupsLevel(match, matchAbove);
            else 
                CompareLineupsCategory(match, matchAbove);
        }

        private void CompareLineupsLevel(TeamMatch match, TeamMatch matchAbove)
        {
            var matchPlayerPositions = GetPlayerPositions(match, false);
            var matchAbovePlayerPositions = GetPlayerPositions(matchAbove, false);
            
            foreach (var playerPos in matchPlayerPositions)
            {
                foreach (var abovePlayerPos in matchAbovePlayerPositions)
                {
                    if (ComparePositionLists(playerPos.Value, abovePlayerPos.Value))
                    {
                        if(!CheckPoints(playerPos.Key.Rankings.LevelPoints, abovePlayerPos.Key.Rankings.LevelPoints))
                            AddRuleBreaks(match, playerPos.Key, matchAbove, abovePlayerPos.Key);
                    }
                }
            }
        }

        private bool CheckPoints(int points, int comparePoints)
        {
            return (points - comparePoints) <= 50;
        }

        private void AddRuleBreaks(TeamMatch match, Player ruleBreaker, TeamMatch matchAbove, Player conflictPlayer)
        {
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if(pos.Player.Member.Id == ruleBreaker.Member.Id)
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Player has too many points compared to {conflictPlayer.Member.Name} on lineup {matchAbove.TeamIndex}"));
                    if(Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer.Member.Id == group.Positions.Count)
                        _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Player has too many points compared to {conflictPlayer.Member.Name} on lineup {matchAbove.TeamIndex}"));
                }
            }
        }


        private Dictionary<Player, List<Lineup.PositionType>> GetPlayerPositions(TeamMatch match, bool ignoreReserves)
        {
            var matchPlayerPositions = new Dictionary<Player, List<Lineup.PositionType>>();
            foreach (var group in match.Lineup)
            {
                foreach (var pos in group.Positions)
                {
                    if (pos.Player != null)
                    {
                        if (!(ignoreReserves && pos.IsExtra))
                        {
                            if (matchPlayerPositions.ContainsKey(pos.Player))
                                matchPlayerPositions[pos.Player].Add(group.Type);
                            else
                                matchPlayerPositions.Add(pos.Player, new List<Lineup.PositionType>() { group.Type });
                        }
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null)
                    {
                        if (!(ignoreReserves && pos.OtherIsExtra))
                        {
                            if (matchPlayerPositions.ContainsKey(pos.OtherPlayer))
                                matchPlayerPositions[pos.OtherPlayer].Add(group.Type);
                            else
                                matchPlayerPositions.Add(pos.OtherPlayer, new List<Lineup.PositionType>() { group.Type });
                        }
                    }
                }
            }
            return matchPlayerPositions;
        }

        private bool ComparePositionLists(List<Lineup.PositionType> pos, List<Lineup.PositionType> upperPos)
        {
            return pos.Count(p => upperPos.Contains(p)) > 1;
        }

        private void CompareLineupsCategory(TeamMatch match, TeamMatch matchAbove)
        {
            var matchPlayerPositions = GetPlayerPositions(match, true);
            var matchAbovePlayerPositions = GetPlayerPositions(matchAbove, true);

            foreach (var playerPos in matchAbovePlayerPositions)
            {
                var playerToCompareWith = matchAbovePlayerPositions.Where(p => p.Key.Sex == playerPos.Key.Sex)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (var playerCompare in playerToCompareWith)
                {
                    if (!CompareUpperPlayerPositions(playerPos, playerCompare))
                    {
                        AddRuleBreaks(match, playerPos.Key, matchAbove, playerCompare.Key);
                    }
                }
            }
        }

        private bool CompareUpperPlayerPositions(KeyValuePair<Player, List<Lineup.PositionType>> playerPos, KeyValuePair<Player, List<Lineup.PositionType>> playerCompare)
        {
            foreach (var comparePositionType in playerCompare.Value)
            {
                if (ComparePlayerPointsType(playerPos.Key, playerCompare.Key, comparePositionType))
                    return true;
            }
            return false;
        }

        private bool ComparePlayerPointsType(Player player, Player comparePlayer, Lineup.PositionType comparePositionType)
        {
            if (comparePositionType == Lineup.PositionType.MixDouble)
                return CheckPoints(player.Rankings.MixPoints, comparePlayer.Rankings.MixPoints);
            else if (Lineup.PositionType.Double.HasFlag(comparePositionType))
                return CheckPoints(player.Rankings.DoublesPoints, comparePlayer.Rankings.DoublesPoints);
            else 
                return CheckPoints(player.Rankings.SinglesPoints, comparePlayer.Rankings.SinglesPoints);
        }
    }
}
