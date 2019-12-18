using Common.Model;

namespace Common
{
    public class RuleBreak
    {
        public (Lineup.PositionType type, int index) Position { get; set; }
        public int PositionIndex { get; set; }
        public string ErrorMessage { get; set; }

        public RuleBreak((Lineup.PositionType type, int index) position, int positionindex, string error)
        {
            Position = position;
            PositionIndex = positionindex;
            ErrorMessage = error;
        }

        public RuleBreak() { }
    }
}
