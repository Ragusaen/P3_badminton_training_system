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
        public void LineupSingleCheck(List<IPosition> list)
        {

            foreach (IPosition position in list)
            {
                foreach()
            }
        }

        public void LineupDoubleCheck(List<IPosition> list)
        {
            foreach (IPosition position in list)
            {
                for (int i = list.IndexOf(position) + 1; i < list.Count; i++)
                    DoubleCheck(position.Player.First(), position.Player.Last(), list[i].Player.First(), list[i].Player.Last());
            }
        }
        public void LineupMixCheck(List<IPosition> list)
        {
            foreach (IPosition position in list)
            {
                for (int i = list.IndexOf(position) + 1; i < list.Count; i++)
                    MixCheck(position.Player.First(), position.Player.Last(), list[i].Player.First(), list[i].Player.Last());
            }
        }
        public void SingleCheck(Player UpperPlayer, Player LowerPlayer, Lineup lineup)
        {
            if (lineup.League >= Leagues.Devision1 || lineup.League < Leagues.DenmarkSeries && UpperPlayer.RankingSingle.Points < (LowerPlayer.RankingSingle.Points - 50) ||
                lineup.League < Leagues.Devision1 || lineup.League >= Leagues.DenmarkSeries && UpperPlayer.RankingLevel.Points < (LowerPlayer.RankingLevel.Points - 50))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Lineup lineup)
        {
            if (lineup.League >= Leagues.Devision1 || lineup.League < Leagues.DenmarkSeries && UpperPlayer1.RankingDouble.Points + UpperPlayer2.RankingDouble.Points < (LowerPlayer1.RankingDouble.Points + LowerPlayer2.RankingDouble.Points - 100) ||
                lineup.League < Leagues.Devision1 || lineup.League >= Leagues.DenmarkSeries && UpperPlayer1.RankingLevel.Points + UpperPlayer2.RankingLevel.Points < (LowerPlayer1.RankingLevel.Points + LowerPlayer2.RankingLevel.Points - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
        public void MixCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Lineup lineup)
        {
            if (lineup.League >= Leagues.Devision1 || lineup.League < Leagues.DenmarkSeries && UpperPlayer1.RankingMixed.Points + UpperPlayer2.RankingMixed.Points < (LowerPlayer1.RankingMixed.Points + LowerPlayer2.RankingMixed.Points - 100) ||
                lineup.League < Leagues.Devision1 || lineup.League >= Leagues.DenmarkSeries && UpperPlayer1.RankingLevel.Points + UpperPlayer2.RankingLevel.Points < (LowerPlayer1.RankingLevel.Points + LowerPlayer2.RankingLevel.Points - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}
