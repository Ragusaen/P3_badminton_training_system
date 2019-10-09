using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            CheckSameSex(lineup.MensSingle, Convert.ToByte("M"));
            CheckSameSex(lineup.WomensSingle, Convert.ToByte("W"));
            CheckSameSex(lineup.MensDouble, Convert.ToByte("M"));
            CheckSameSex(lineup.WomensDouble, Convert.ToByte("W"));
            CheckMixSex(lineup.MixDouble);
            return RuleBreaks;
        }
        public void CheckSameSex(List<Player> List, byte sex)
        {
            foreach (Player Player in List)
            {
                if (Player.Member.Sex != sex)
                    RuleBreaks.Add(new RuleBreak(Player, ErrorMessage));
            }
        }

        public void CheckMixSex(List<Player> List)
        {
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (i % 2 == 1 && List[i].Member.Sex != Convert.ToByte("M"))
                    RuleBreaks.Add(new RuleBreak(List[i], ErrorMessage));
                else if (i % 2 == 0 && List[i].Member.Sex != Convert.ToByte("W"))
                    RuleBreaks.Add(new RuleBreak(List[i], ErrorMessage));
            }
        }
    }
}
