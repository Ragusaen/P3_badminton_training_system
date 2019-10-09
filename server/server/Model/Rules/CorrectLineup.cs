using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace server.Model.Rules
{
    class CorrectLineup : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            LineupSingleCheck(lineup.MensSingle);
            LineupSingleCheck(lineup.WomensSingle);
            LineupDoubleCheck(lineup.MensDouble);
            LineupDoubleCheck(lineup.WomensDouble);
            LineupDoubleCheck(lineup.MixDouble);
            return RuleBreaks;
        }
        public void LineupSingleCheck(List<Player> List)
        {
            for (int i = 0; i > List.Count - 2; i++)
            {
                SingleCheck(List[i], List[i + 1]);
            }
        }

        public void LineupDoubleCheck(List<Player> List)
        {
            for (int i = 0; i > List.Count - 4; i += 2)
            {
                if (List[i + 3] != null)
                    DoubleCheck(List[i], List[i + 1], List[i + 2], List[i + 3]);
            }
        }

        public void SingleCheck(Player UpperPlayer, Player LowerPlayer)
        {
            if (UpperPlayer.RankingSingle.Points < (LowerPlayer.RankingSingle.Points - 50))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2)
        {
            if (UpperPlayer1.RankingDouble.Points + UpperPlayer2.RankingDouble.Points < (LowerPlayer1.RankingDouble.Points + LowerPlayer2.RankingDouble.Points - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}
