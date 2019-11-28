using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class LineupPointsRule : IRule
    {
        public int Priority { get; set; } = 10;
        private int _maxSingleDiff;
        private int _maxDoubleDiff;
        private List<PlayerRanking.AgeGroup> _ignoreAgeGroups;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public LineupPointsRule(int maxSingleDiff, int maxDoubleDiff, List<PlayerRanking.AgeGroup> ignoreAgeGroups)
        {
            _maxSingleDiff = maxSingleDiff;
            _maxDoubleDiff = maxDoubleDiff;
            _ignoreAgeGroups = ignoreAgeGroups;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            foreach (var group in match.Lineup)
            {
                CheckPositions(group.Positions, group.Type);
            }

            return _ruleBreaks;
        }

        private bool CheckPositions(List<Position> positions, Lineup.PositionType type)
        {
            bool success = true;
            for (int i = 1; i < positions.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (!ComparePositions(positions[i], positions[j], type))
                    {
                        success = false;
                        if (Lineup.PositionType.Double.HasFlag(type))
                        {
                            _ruleBreaks.Add(new RuleBreak((type, i), 0, $"Double has too many points compared to position {j+1}"));
                            _ruleBreaks.Add(new RuleBreak((type, i), 1, $"Double has too many points compared to position {j+1}"));
                        }
                        else
                            _ruleBreaks.Add(new RuleBreak((type, i), 0, $"Player has too many points compared to position {j+1}"));
                        break;
                    }
                }
            }

            return success;
        }

        private bool ComparePositions(Position lower, Position upper, Lineup.PositionType type)
        {
            //If single, positions is not empty and players age group is not ignored
            if (Lineup.PositionType.Single.HasFlag(type) && lower.Player != null && upper.Player != null &&
                !ContainsIgnoredAgeGroup(lower.Player.Rankings.Age) && !ContainsIgnoredAgeGroup(upper.Player.Rankings.Age))
            {
                return (lower.Player.Rankings.SinglesPoints - upper.Player.Rankings.SinglesPoints) <= _maxSingleDiff;
            }

            //If mix double, positions is not empty and players age group is not ignored
            if (type == Lineup.PositionType.MixDouble && lower.Player != null && lower.OtherPlayer != null &&
                upper.Player != null && upper.OtherPlayer != null &&
                !ContainsIgnoredAgeGroup(lower.Player.Rankings.Age) && !ContainsIgnoredAgeGroup(upper.Player.Rankings.Age) &&
                !ContainsIgnoredAgeGroup(lower.OtherPlayer.Rankings.Age) && !ContainsIgnoredAgeGroup(upper.OtherPlayer.Rankings.Age))
            {
                return ((lower.Player.Rankings.MixPoints + lower.OtherPlayer.Rankings.MixPoints)
                        - (upper.Player.Rankings.MixPoints + upper.OtherPlayer.Rankings.MixPoints))
                       <= _maxDoubleDiff;
            }

            //If double, positions is not empty and players age group is not ignored
            if (Lineup.PositionType.Double.HasFlag(type) && lower.Player != null && lower.OtherPlayer != null &&
                upper.Player != null && upper.OtherPlayer != null &&
                !ContainsIgnoredAgeGroup(lower.Player.Rankings.Age) && !ContainsIgnoredAgeGroup(upper.Player.Rankings.Age) &&
                !ContainsIgnoredAgeGroup(lower.OtherPlayer.Rankings.Age) && !ContainsIgnoredAgeGroup(upper.OtherPlayer.Rankings.Age))
            {
                return ((lower.Player.Rankings.DoublesPoints + lower.OtherPlayer.Rankings.DoublesPoints)
                        - (upper.Player.Rankings.DoublesPoints + upper.OtherPlayer.Rankings.DoublesPoints)) <= _maxDoubleDiff;
            }
                
            return true;
        }

        private bool ContainsIgnoredAgeGroup(PlayerRanking.AgeGroup age)
        {
            return _ignoreAgeGroups.Contains(age);
        }
    }
}
