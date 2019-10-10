using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model.Positions;
using Server.Model;
using static server.Model.Lineup;
using static Server.Model.Player;

namespace server.Model.Rules
{
    class AgeRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            if (lineup.League <= Leagues.DenmarkSeries)
            {
                CheckPlayerAge(lineup, AgeGroup.U17);
                return RuleBreaks;
            }
            else 
            {
                CheckPlayerAge(lineup, AgeGroup.U17, AgeGroup.U15);
                return RuleBreaks;
            }
        }
        public void CheckPlayerAge(Lineup lineup, AgeGroup warningAge) { CheckPlayerAge(lineup, warningAge, warningAge); }
        public void CheckPlayerAge(Lineup lineup, AgeGroup warningAge, AgeGroup errorAge)
        {
            foreach (Position Position in lineup.Positions) {
                foreach (Player Player in Position.Player)
                {
                    if (Player.Age <= warningAge && Player.Age >= errorAge) {
                        ErrorMessage = "Warning because of player age placeing is subjektiv, may require exemption";
                        RuleBreaks.Add(new RuleBreak(Player, ErrorMessage));
                    }
                    else if (Player.Age < errorAge) {
                        ErrorMessage = "Error player age is to low";
                        RuleBreaks.Add(new RuleBreak(Player, ErrorMessage));
                    }
                }
            }
        }
    }
}
