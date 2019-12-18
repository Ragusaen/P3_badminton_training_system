using Common.Model;

namespace Common
{
    public class RuleBreak
    {
        public (Lineup.PositionType type, int index) Position { get; set; }
        public int PlayerIndex { get; set; }
        public string ErrorMessage { get; set; }

        public RuleBreak((Lineup.PositionType type, int index) position, int positionindex, string error)
        {
            Position = position;
            PlayerIndex = positionindex;
            ErrorMessage = error;
        }

        public RuleBreak() { }
    }
}
