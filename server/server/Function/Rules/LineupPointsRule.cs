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

        public LineupPointsRule(int maxSingleDiff, int maxDoubleDiff)
        {
            _maxSingleDiff = maxSingleDiff;
            _maxDoubleDiff = maxDoubleDiff;
        }

        public List<RuleBreak> Rule(Match match)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();

            foreach (var position in match.Lineup.Positions)
            {
                if (position.Value is Position dp)
                {
                    for (int i = position.Key.Item2 - 1; i >= 1; i--)
                    {
                        if (!CheckDouble(position.Value, match.Lineup.Positions[new Tuple<Lineup.PositionType, int>(position.Key.Item1, i)], position.Key.Item1))
                        {
                            ruleBreaks.Add(new RuleBreak(position.Key, 0, $"Double has too many points compared to double at position {i}!"));
                            ruleBreaks.Add(new RuleBreak(position.Key, 1, $"Double has too many points compared to double at position {i}!"));
                            ruleBreaks.Add(new RuleBreak(new Tuple<Lineup.PositionType, int>(position.Key.Item1, i), 0, $"Double has too few points compared to double at position {i}!"));
                            ruleBreaks.Add(new RuleBreak(new Tuple<Lineup.PositionType, int>(position.Key.Item1, i), 1, $"Double has too few points compared to double at position {i}!"));
                        }
                    }
                }
                else
                {
                    for (int i = position.Key.Item2 - 1; i >= 1; i--)
                    {
                        if (!CheckSingle(position.Value.Player, match.Lineup.Positions[new Tuple<Lineup.PositionType, int>(position.Key.Item1, i)].Player))
                        {
                            ruleBreaks.Add(new RuleBreak(position.Key, 0, $"Player has too many points compared to the player at position {i}!"));
                            ruleBreaks.Add(new RuleBreak(new Tuple<Lineup.PositionType, int>(position.Key.Item1, i), 0, $"Player has too many points compared to the player at position {i}!"));
                        }
                    }
                }
            }
            return ruleBreaks;
        }

        private bool CheckSingle(Player lowerPlayer, Player upperPlayer)
        {
            return (lowerPlayer.Rankings.SinglesPoints - upperPlayer.Rankings.SinglesPoints) <= _maxSingleDiff;
        }

        private bool CheckDouble(Position lowerDouble, Position upperDouble, Lineup.PositionType positionType)
        {
            if (positionType == Lineup.PositionType.MixDouble)
                return ((lowerDouble.Player.Rankings.MixPoints + lowerDouble.OtherPlayer.Rankings.MixPoints) - (upperDouble.Player.Rankings.MixPoints + upperDouble.OtherPlayer.Rankings.MixPoints)) <= _maxDoubleDiff;
            else
                return ((lowerDouble.Player.Rankings.DoublesPoints + lowerDouble.OtherPlayer.Rankings.DoublesPoints) - (upperDouble.Player.Rankings.DoublesPoints + upperDouble.OtherPlayer.Rankings.DoublesPoints)) <= _maxDoubleDiff;
        }
    }
}
