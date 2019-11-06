using Common.Model.Member;

namespace Common.Model
{
    class RuleBreak
    {
        public Player Player { get; set; }
        public string Brake { get; set; }

        public RuleBreak(Player player, string brake)
        {
            Player = player;
            Brake = brake;
        }
    }
}
