using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class LineupPointsRule : IRule
    {
        public int Priority { get; set; }
        private int _maxSingleDiff;
        private int _maxDoubleDiff;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();


        public LineupPointsRule(int maxSingleDiff, int maxDoubleDiff)
        {
            _maxSingleDiff = maxSingleDiff;
            _maxDoubleDiff = maxDoubleDiff;
        }

        public List<RuleBreak> Rule(TeamMatch match)
        {

            foreach (var group in match.Lineup)
            {
                CheckPositions(group.positions, group.type);
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
                            _ruleBreaks.Add(new RuleBreak((type, i), 0, $"Double has too many points compared to position {j}"));
                            _ruleBreaks.Add(new RuleBreak((type, i), 1, $"Double has too many points compared to position {j}"));
                        }
                        else
                            _ruleBreaks.Add(new RuleBreak((type, i), 0, $"Player has too many points compared to position {j}"));
                        break;
                    }
                }
            }

            return success;
        }

        private bool ComparePositions(Position lower, Position upper, Lineup.PositionType type)
        {
            if (Lineup.PositionType.Single.HasFlag(type))
                return (lower.Player.Rankings.SinglesPoints - upper.Player.Rankings.SinglesPoints) <= _maxSingleDiff;
            if (type == Lineup.PositionType.MixDouble)
                return ((lower.Player.Rankings.MixPoints + lower.OtherPlayer.Rankings.MixPoints) 
                        - (upper.Player.Rankings.MixPoints + upper.OtherPlayer.Rankings.MixPoints))
                       <= _maxDoubleDiff;
            else
                return ((lower.Player.Rankings.DoublesPoints + lower.OtherPlayer.Rankings.DoublesPoints)
                        - (upper.Player.Rankings.DoublesPoints + upper.OtherPlayer.Rankings.DoublesPoints)) <= _maxDoubleDiff;
        }
    }
}
