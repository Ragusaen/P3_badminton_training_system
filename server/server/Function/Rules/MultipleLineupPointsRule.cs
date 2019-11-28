using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.DAL;

namespace Server.Function.Rules
{
    class MultipleLineupsPointsRule : IRule
    {
        public int Priority { get; set; } = 11;
        private List<PlayerRanking.AgeGroup> _ignoreAgeGroups;
        private RankingCompareType _rankingCompareType;
        private PlayersToCompare _playersToCompare;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();
        

        public MultipleLineupsPointsRule(List<PlayerRanking.AgeGroup> ignoredAgeGroups, RankingCompareType rankingCompareType, PlayersToCompare playersToCompare)
        {
            _ignoreAgeGroups = ignoredAgeGroups;
            _rankingCompareType = rankingCompareType;
            _playersToCompare = playersToCompare;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            var db = new DatabaseEntities();
            List<TeamMatch> otherMatches = db.teammatches
                .Where(m => m.Season == match.Season && m.LeagueRound == match.LeagueRound && m.TeamIndex != match.TeamIndex)
                .ToList()
                .Select(p => (TeamMatch)p)
                .ToList();
            
            foreach (TeamMatch teamMatch in otherMatches)
                CompareLineups(match, teamMatch);

            return _ruleBreaks;
        }

        private void CompareLineups(TeamMatch match, TeamMatch otherMatch)
        {
            //If upper lineup is placed Division 1 or above, compare category. Else compare niveau.
            if (match.TeamIndex > otherMatch.TeamIndex)
            {
                if (_rankingCompareType == RankingCompareType.Level)
                    CompareLineupsLevel(match, otherMatch, true);
                else
                    CompareLineupsCategory(match, otherMatch, true);
            }
            else
            {
                if (_rankingCompareType == RankingCompareType.Level)
                    CompareLineupsLevel(otherMatch, match, false);
                else
                    CompareLineupsCategory(otherMatch, match, false);
            }
        }

        private void CompareLineupsLevel(TeamMatch match, TeamMatch matchAbove, bool isLower)
        {
            var matchPlayerPositions = GetPlayerPositions(match, false);
            var matchAbovePlayerPositions = GetPlayerPositions(matchAbove, false);
            
            foreach (var playerPos in matchPlayerPositions)
            {
                Dictionary<Player, List<Lineup.PositionType>> playersToCompare = GetPlayerToCompare(playerPos, matchAbovePlayerPositions);

                foreach (var abovePlayerPos in playersToCompare)
                {
                    if(!CheckPoints(playerPos.Key.Rankings.LevelPoints, abovePlayerPos.Key.Rankings.LevelPoints))
                        AddRuleBreaks(match, playerPos.Key, matchAbove, abovePlayerPos.Key, isLower);
                }
            }
        }

        private bool CheckPoints(int points, int comparePoints)
        {
            return (points - comparePoints) <= 50;
        }

        private void AddRuleBreaks(TeamMatch match, Player player, TeamMatch matchAbove, Player playerAbove, bool isLower)
        {
            if (isLower)
            {
                foreach (var group in match.Lineup)
                {
                    for (int i = 0; i < group.Positions.Count; i++)
                    {
                        var pos = group.Positions[i];
                        if (pos.Player.Member.Id == player.Member.Id)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Player has too many points compared to {playerAbove.Member.Name} on lineup {matchAbove.TeamIndex}"));
                        if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer.Member.Id == group.Positions.Count)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Player has too many points compared to {playerAbove.Member.Name} on lineup {matchAbove.TeamIndex}"));
                    }
                }
            }
            else
            {
                foreach (var group in matchAbove.Lineup)
                {
                    for (int i = 0; i < group.Positions.Count; i++)
                    {
                        var pos = group.Positions[i];
                        if (pos.Player.Member.Id == playerAbove.Member.Id)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Too little points compared to player {player.Member.Name} on lineup {match.TeamIndex}"));
                        if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer.Member.Id == group.Positions.Count)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Too little points compared to player {player.Member.Name} on lineup {match.TeamIndex}"));
                    }
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
                    if (pos.Player != null && !ContainsIgnoredAgeGroup(pos.Player.Rankings.Age))
                    {
                        if (!(ignoreReserves && pos.IsExtra))
                        {
                            if (matchPlayerPositions.ContainsKey(pos.Player))
                                matchPlayerPositions[pos.Player].Add(group.Type);
                            else
                                matchPlayerPositions.Add(pos.Player, new List<Lineup.PositionType>() { group.Type });
                        }
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null && !ContainsIgnoredAgeGroup(pos.OtherPlayer.Rankings.Age))
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
        private bool ContainsIgnoredAgeGroup(PlayerRanking.AgeGroup age)
        {
            return _ignoreAgeGroups.Contains(age);
        }

        private void CompareLineupsCategory(TeamMatch match, TeamMatch matchAbove, bool isLower)
        {
            var matchPlayerPositions = GetPlayerPositions(match, true);
            var matchAbovePlayerPositions = GetPlayerPositions(matchAbove, true);

            foreach (var playerPos in matchPlayerPositions)
            {
                Dictionary<Player, List<Lineup.PositionType>> playersToCompare = GetPlayerToCompare(playerPos, matchAbovePlayerPositions);

                foreach (var playerCompare in playersToCompare)
                {
                    if (!CompareAbovePlayerPositions(playerPos, playerCompare))
                    {
                        AddRuleBreaks(match, playerPos.Key, matchAbove, playerCompare.Key, isLower);
                    }
                }
            }
        }

        private Dictionary<Player, List<Lineup.PositionType>> GetPlayerToCompare(KeyValuePair<Player, List<Lineup.PositionType>> playerPos, Dictionary<Player, List<Lineup.PositionType>> matchAbovePlayerPositions)
        {
            Dictionary<Player, List<Lineup.PositionType>> playersToCompare = new Dictionary<Player, List<Lineup.PositionType>>();

            if (_playersToCompare == PlayersToCompare.SameSex)
                playersToCompare = matchAbovePlayerPositions.Where(p => p.Key.Sex == playerPos.Key.Sex)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            else
                playersToCompare = matchAbovePlayerPositions.Where(p => playerPos.Value.Count(q => p.Value.Contains(q)) > 1).ToDictionary(pair => pair.Key, pair => pair.Value); ;
            return playersToCompare;
        }

        private bool CompareAbovePlayerPositions(KeyValuePair<Player, List<Lineup.PositionType>> playerPos, KeyValuePair<Player, List<Lineup.PositionType>> playerCompare)
        {
            int count = 0;
            foreach (var comparePositionType in playerCompare.Value)
            {
                if (ComparePlayerPointsType(playerPos.Key, playerCompare.Key, comparePositionType))
                    count++;
            }

            if (_rankingCompareType == RankingCompareType.CategoryOne)
                return count >= 1;
            if (_rankingCompareType == RankingCompareType.CategoryBoth)
                return count >= 2;
            return true;
        }

        private bool ComparePlayerPointsType(Player player, Player comparePlayer, Lineup.PositionType comparePositionType)
        {
            if (comparePositionType == Lineup.PositionType.MixDouble)
                return CheckPoints(player.Rankings.MixPoints, comparePlayer.Rankings.MixPoints);
            if (Lineup.PositionType.Double.HasFlag(comparePositionType))
                return CheckPoints(player.Rankings.DoublesPoints, comparePlayer.Rankings.DoublesPoints);
            return CheckPoints(player.Rankings.SinglesPoints, comparePlayer.Rankings.SinglesPoints);
        }
    }
}
