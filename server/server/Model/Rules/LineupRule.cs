using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;
using static server.Model.Lineup;

namespace server.Model.Rules
{
    class LineupRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {

            return RuleBreaks;
        }
        public void LineupSingleCheck(List<IPosition> list, List<IPosition> list2, Leagues league)
        {

            foreach (IPosition position in list)
            {
                foreach (IPosition position2 in list2) 
                {
                    SingleCheck(position.Player.First(), position2.Player.First(), league);
                }
            }
        }

        public void LineupDoubleCheck(List<IPosition> list, List<IPosition> list2, Leagues league)
        {
            foreach (IPosition position in list)
            {
                foreach (IPosition position2 in list2)
                {
                    DoubleCheck(position.Player.First(), position.Player.Last(), position2.Player.First(), position2.Player.Last(), league);
                }
            }
        }
        public void LineupMixCheck(List<IPosition> list, List<IPosition> list2, Leagues league)
        {
            foreach (IPosition position in list)
            {
                foreach (IPosition position2 in list2)
                {
                    MixCheck(position.Player.First(), position.Player.Last(), position2.Player.First(), position2.Player.Last(), league);
                }
            }
        }
        public void SingleCheck(Player UpperPlayer, Player LowerPlayer, Leagues league)
        {
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer.RankingSingle.Points < (LowerPlayer.RankingSingle.Points - 50) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer.RankingLevel.Points < (LowerPlayer.RankingLevel.Points - 50))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Leagues league)
        {
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer1.RankingDouble.Points + UpperPlayer2.RankingDouble.Points < (LowerPlayer1.RankingDouble.Points + LowerPlayer2.RankingDouble.Points - 100) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer1.RankingLevel.Points + UpperPlayer2.RankingLevel.Points < (LowerPlayer1.RankingLevel.Points + LowerPlayer2.RankingLevel.Points - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
        public void MixCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Leagues league)
        {
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer1.RankingMixed.Points + UpperPlayer2.RankingMixed.Points < (LowerPlayer1.RankingMixed.Points + LowerPlayer2.RankingMixed.Points - 100) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer1.RankingLevel.Points + UpperPlayer2.RankingLevel.Points < (LowerPlayer1.RankingLevel.Points + LowerPlayer2.RankingLevel.Points - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}
