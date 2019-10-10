using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Model.Positions;
using Server.Model;

namespace server.Model.Rules
{
    class SexRule : IRule
    {
        public string ErrorMessage { get; set; }

        public List<RuleBreak> RuleBreaks { get; set; } = new List<RuleBreak>();

        SexRule() 
        {
            ErrorMessage = "Is Not the correct gender";
        }

        public List<RuleBreak> Rule(Lineup lineup)
        {

            CheckSameSex(lineup);
            CheckMixSex(lineup);
            return RuleBreaks;
        }
        public void CheckSameSex(Lineup lineup)
        {
            foreach (Position position in lineup.Positions)
            {
                if (!(position is MixDouble))
                {
                    foreach (Player player in position.Player)
                    {
                        if (player.Member.Sex != Convert.ToByte("M") && ((position is MensSingle) || (position is MensDouble)))
                            RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                        else if (player.Member.Sex != Convert.ToByte("W") && ((position is WomensSingle) || (position is WomensDouble)))
                            RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                    }
                }
            }
        }

        public void CheckMixSex(Lineup lineup)
        {
            bool first = true;
            foreach (MixDouble Mix in lineup.Positions)
            {
                first = true;
                foreach (Player Player in Mix.Player)
                {
                    if (first && Player.Member.Sex != Convert.ToByte("M"))
                        RuleBreaks.Add(new RuleBreak(Mix.Player[1], ErrorMessage));
                    else if (!first && Player.Member.Sex != Convert.ToByte("W"))
                        RuleBreaks.Add(new RuleBreak(Mix.Player[2], ErrorMessage));
                    first = false;
                }
            }
        }
    }
}
