using System.Collections.Generic;
using Common.Model.Member;
using Common.Model.Positions;

namespace Common.Model.Rules
{
    class AgeRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            if (lineup.League <= Lineup.Leagues.DenmarkSeries)
            {
                CheckPlayerAge(lineup, Player.AgeGroup.U17);
                return RuleBreaks;
            }
            else 
            {
                CheckPlayerAge(lineup, Player.AgeGroup.U17, Player.AgeGroup.U15);
                return RuleBreaks;
            }
        }
        public void CheckPlayerAge(Lineup lineup, Player.AgeGroup warningAge) { CheckPlayerAge(lineup, warningAge, warningAge); }
        public void CheckPlayerAge(Lineup lineup, Player.AgeGroup warningAge, Player.AgeGroup errorAge)
        {
            foreach (IPosition Position in lineup.Positions) {
                foreach (Player player in Position.Player)
                {
                    if (player.Age <= warningAge && player.Age >= errorAge) {
                        ErrorMessage = "Warning because of player age placeing is subjektiv, may require exemption";
                        RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                    }
                    else if (player.Age < errorAge) {
                        ErrorMessage = "Error player age is to low";
                        RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                    }
                }
            }
        }
    }
}
