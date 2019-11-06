using Common.Model;
using System;

namespace Server.Function.Rules
{
    class RuleBreak
    {
        public Tuple<Lineup.PositionType, int> Position { get; set; }
        public int PositionIndex { get; set; }
        public string ErrorMessage { get; set; }

        public RuleBreak(Tuple<Lineup.PositionType, int> position, int positionIndex, string error)
        {
            Position = position;
            PositionIndex = positionIndex;
            ErrorMessage = error;
        }
    }
}
