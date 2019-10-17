using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Server.DAL;
using Server.Model;
using Server.Model.Positions;
using static Server.Model.Lineup;

namespace Server.Model.Rules
{
    class LineupRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            Lineup lineup2 = new Lineup();
            LineupSingleCheck(lineup.Positions.Where(p => p is MensSingle).ToList(), lineup2.Positions.Where(p => p is MensSingle).ToList(), lineup.League);
            LineupSingleCheck(lineup.Positions.Where(p => p is WomensSingle).ToList(), lineup2.Positions.Where(p => p is WomensSingle).ToList(), lineup.League);
            LineupDoubleCheck(lineup.Positions.Where(p => p is MensDouble).ToList(), lineup2.Positions.Where(p => p is MensDouble).ToList(), lineup.League);
            LineupDoubleCheck(lineup.Positions.Where(p => p is WomensDouble).ToList(), lineup2.Positions.Where(p => p is WomensDouble).ToList(), lineup.League);
            LineupMixCheck(lineup.Positions.Where(p => p is MixDouble).ToList(), lineup2.Positions.Where(p => p is MixDouble).ToList(), lineup.League);
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
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer.Rankings.SinglesPoints < (LowerPlayer.Rankings.SinglesPoints - 50) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer.Rankings.LevelPoints < (LowerPlayer.Rankings.LevelPoints - 50))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Leagues league)
        {
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer1.Rankings.DoublesPoints + UpperPlayer2.Rankings.DoublesPoints < (LowerPlayer1.Rankings.DoublesPoints + LowerPlayer2.Rankings.DoublesPoints - 100) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer1.Rankings.LevelPoints + UpperPlayer2.Rankings.LevelPoints < (LowerPlayer1.Rankings.LevelPoints + LowerPlayer2.Rankings.LevelPoints - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
        public void MixCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2, Leagues league)
        {
            if (league >= Leagues.Devision1 || league < Leagues.DenmarkSeries && UpperPlayer1.Rankings.MixPoints + UpperPlayer2.Rankings.MixPoints < (LowerPlayer1.Rankings.MixPoints + LowerPlayer2.Rankings.MixPoints - 100) ||
                league < Leagues.Devision1 || league >= Leagues.DenmarkSeries && UpperPlayer1.Rankings.LevelPoints + UpperPlayer2.Rankings.LevelPoints < (LowerPlayer1.Rankings.LevelPoints + LowerPlayer2.Rankings.LevelPoints - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}
