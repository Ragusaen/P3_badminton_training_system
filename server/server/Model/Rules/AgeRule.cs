using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace server.Model.Rules
{
    class AgeRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            if (lineup.League < 6)
            {
                CheckPlayerAge(lineup.MensSingle, 17, 16);
                CheckPlayerAge(lineup.MensDouble, 17, 16);
                CheckPlayerAge(lineup.WomensSingle, 17, 16);
                CheckPlayerAge(lineup.WomensDouble, 17, 16);
                CheckPlayerAge(lineup.MixDouble, 17, 16);
                return RuleBreaks;
            }
            else 
            {
                CheckPlayerAge(lineup.MensSingle, 17, 15);
                CheckPlayerAge(lineup.MensDouble, 17, 15);
                CheckPlayerAge(lineup.WomensSingle, 17, 15);
                CheckPlayerAge(lineup.WomensDouble, 17, 15);
                CheckPlayerAge(lineup.MixDouble, 17, 15);
                return RuleBreaks;
            }
            

        }
        public void CheckPlayerAge(List<Player> List, int age1, int age2)
        {
            foreach (Player Player in List)
            {
                if (Player.Age <= age1 && Player.Age >= age2) {
                    ErrorMessage = "Warning because of player age placeing is subjektiv";
                    RuleBreaks.Add(new RuleBreak(Player, ErrorMessage));
                }
                else if (Player.Age < age2) {
                    ErrorMessage = "Error player age is to low";
                    RuleBreaks.Add(new RuleBreak(Player, ErrorMessage));
                }
            }
        }
    }
}
