using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.DAL;

namespace Server.Function.Rules
{
    class MultipleLineupsPointsRule : IRule
    {
        enum PlayersToCompare
        {
            SameSex,
            SameCategory
        }

        enum RankingCompareType
        {
            CategoryOne,
            CategoryBoth,
            Level
        }

        public int Priority { get; set; } = 13;
        private List<PlayerRanking.AgeGroup> _ignoreAgeGroups;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();
        

        public MultipleLineupsPointsRule(List<PlayerRanking.AgeGroup> ignoredAgeGroups)
        {
            _ignoreAgeGroups = ignoredAgeGroups;
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
            if (match.TeamIndex > otherMatch.TeamIndex)
            {
                CompareLineupByLeague(match, otherMatch, true);
            }
            else
            {
                CompareLineupByLeague(otherMatch, match, false);
            }
        }

        private void CompareLineupByLeague(TeamMatch lower, TeamMatch higher, bool isLower)
        {
            switch (higher.League)
            {
                case TeamMatch.Leagues.BadmintonLeague:
                case TeamMatch.Leagues.Division1:
                    CompareLineups(lower, higher, isLower, PlayersToCompare.SameSex, RankingCompareType.CategoryOne);
                    break;
                case TeamMatch.Leagues.Division2:
                case TeamMatch.Leagues.Division3:
                case TeamMatch.Leagues.DenmarksSeries:
                    CompareLineups(lower, higher, isLower, PlayersToCompare.SameCategory, RankingCompareType.Level);
                    break;
                case TeamMatch.Leagues.RegionalSeriesWest:
                case TeamMatch.Leagues.Series1West:
                    CompareLineups(lower, higher, isLower, PlayersToCompare.SameCategory, RankingCompareType.CategoryOne);
                    break;
            }
        }

        private void CompareLineups(TeamMatch lower, TeamMatch higher, bool isLower, PlayersToCompare compare, RankingCompareType rankingType)
        {
            var lowerPlayerPositions = GetPlayerPositions(lower, true);
            var higherPlayerPositions = GetPlayerPositions(higher, true);

            foreach (var lowerPlayerPosition in lowerPlayerPositions)
            {
                Dictionary<Player, List<Lineup.PositionType>> playersPositionsToCompare =
                    GetPlayerPositionsToCompare(lowerPlayerPosition, higherPlayerPositions, compare);

                foreach (var higherPlayerPos in playersPositionsToCompare)
                {
                    if (rankingType == RankingCompareType.Level)
                    {
                        if(!CheckPoints(lowerPlayerPosition.Key.Rankings.LevelPoints, higherPlayerPos.Key.Rankings.LevelPoints))
                            AddRuleBreaks(lower, lowerPlayerPosition.Key, higher, higherPlayerPos.Key, isLower);
                    }
                    else
                    {
                        if(!CompareAbovePlayerPositions(lowerPlayerPosition, higherPlayerPos, rankingType))
                            AddRuleBreaks(lower, lowerPlayerPosition.Key, higher, higherPlayerPos.Key, isLower);
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
                            if (matchPlayerPositions.Any(p => p.Key.Member.Id == pos.Player.Member.Id))
                                matchPlayerPositions.FirstOrDefault(p => p.Key.Member.Id == pos.Player.Member.Id).Value.Add(group.Type);
                            else
                                matchPlayerPositions.Add(pos.Player, new List<Lineup.PositionType>() { group.Type });
                        }
                    }

                    if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null && !ContainsIgnoredAgeGroup(pos.OtherPlayer.Rankings.Age))
                    {
                        if (!(ignoreReserves && pos.OtherIsExtra))
                        {
                            if (matchPlayerPositions.Any(p => p.Key.Member.Id == pos.OtherPlayer.Member.Id))
                                matchPlayerPositions.FirstOrDefault(p => p.Key.Member.Id == pos.OtherPlayer.Member.Id).Value.Add(group.Type);
                            else
                                matchPlayerPositions.Add(pos.OtherPlayer, new List<Lineup.PositionType>() { group.Type });
                        }
                    }
                }
            }
            return matchPlayerPositions;
        }

        private Dictionary<Player, List<Lineup.PositionType>> GetPlayerPositionsToCompare(KeyValuePair<Player, List<Lineup.PositionType>> lowerPosition, Dictionary<Player, List<Lineup.PositionType>> higherPositions, PlayersToCompare compare)
        {
            var playersToCompare = new Dictionary<Player, List<Lineup.PositionType>>();

            if (compare == PlayersToCompare.SameSex)
                playersToCompare = higherPositions.Where(p => p.Key.Sex == lowerPosition.Key.Sex)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            else if (compare == PlayersToCompare.SameCategory)
                playersToCompare = higherPositions.Where(p => lowerPosition.Value.Count(q => p.Value.Contains(q)) > 1)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

            return playersToCompare;
        }

        private bool CheckPoints(int lowerPoints, int higherPoints)
        {
            return (lowerPoints - higherPoints) <= 50;
        }
        
        private bool CompareAbovePlayerPositions(KeyValuePair<Player, List<Lineup.PositionType>> lowerPos, KeyValuePair<Player, List<Lineup.PositionType>> higherPos, RankingCompareType compareType)
        {
            int count = 0;
            foreach (var comparePositionType in higherPos.Value)
            {
                if (ComparePlayerPointsType(lowerPos.Key, higherPos.Key, comparePositionType))
                    count++;
            }

            if (compareType == RankingCompareType.CategoryOne)
                return count >= 1;
            if (compareType == RankingCompareType.CategoryBoth)
                return count >= 2;
            return true;
        }

        private bool ComparePlayerPointsType(Player lowerPlayer, Player higherPlayer, Lineup.PositionType comparePositionType)
        {
            if (comparePositionType == Lineup.PositionType.MixDouble)
                return CheckPoints(lowerPlayer.Rankings.MixPoints, higherPlayer.Rankings.MixPoints);
            if (Lineup.PositionType.Double.HasFlag(comparePositionType))
                return CheckPoints(lowerPlayer.Rankings.DoublesPoints, higherPlayer.Rankings.DoublesPoints);
            
            return CheckPoints(lowerPlayer.Rankings.SinglesPoints, higherPlayer.Rankings.SinglesPoints);
        }

        private void AddRuleBreaks(TeamMatch lowerMatch, Player lowerPlayer, TeamMatch higherMatch, Player higherPlayer, bool isLower)
        {
            if (isLower)
            {
                foreach (var group in lowerMatch.Lineup)
                {
                    for (int i = 0; i < group.Positions.Count; i++)
                    {
                        var pos = group.Positions[i];
                        if (pos.Player != null && pos.Player.Member.Id == lowerPlayer.Member.Id)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Player has too many points compared to {higherPlayer.Member.Name} on lineup {higherMatch.TeamIndex}"));
                        if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null && pos.OtherPlayer.Member.Id == group.Positions.Count)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Player has too many points compared to {higherPlayer.Member.Name} on lineup {higherMatch.TeamIndex}"));
                    }
                }
            }
            else
            {
                foreach (var group in higherMatch.Lineup)
                {
                    for (int i = 0; i < group.Positions.Count; i++)
                    {
                        var pos = group.Positions[i];
                        if (pos.Player.Member.Id == higherPlayer.Member.Id)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, $"Too little points compared to player {lowerPlayer.Member.Name} on lineup {lowerMatch.TeamIndex}"));
                        if (Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer.Member.Id == group.Positions.Count)
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, $"Too little points compared to player {lowerPlayer.Member.Name} on lineup {lowerMatch.TeamIndex}"));
                    }
                }
            }

        }

        private bool ContainsIgnoredAgeGroup(PlayerRanking.AgeGroup age)
        {
            return _ignoreAgeGroups.Contains(age);
        }
    }
}
