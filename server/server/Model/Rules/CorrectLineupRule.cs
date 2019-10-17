using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model.Positions;
using Server.Model;

namespace Server.Model.Rules
{
    class CorrectLineup : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            LineupSingleCheck(lineup.Positions.Where(p => p is MensSingle).ToList());
            LineupSingleCheck(lineup.Positions.Where(p => p is WomensSingle).ToList());
            LineupDoubleCheck(lineup.Positions.Where(p => p is MensDouble).ToList());
            LineupDoubleCheck(lineup.Positions.Where(p => p is WomensDouble).ToList());
            LineupMixCheck(lineup.Positions.Where(p => p is MixDouble).ToList());
            return RuleBreaks;
        }
        public void LineupSingleCheck(List<IPosition> list)
        {

            foreach (IPosition position in list)
            { 
                for(int i = list.IndexOf(position) + 1; i < list.Count;i++)
                    SingleCheck(position.Player.First(), list[i].Player.First());
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

        public void SingleCheck(Player UpperPlayer, Player LowerPlayer)
        {
            if (UpperPlayer.Rankings.SinglesPoints < (LowerPlayer.Rankings.SinglesPoints - 50))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2)
        {
            if (UpperPlayer1.Rankings.DoublesPoints + UpperPlayer2.Rankings.DoublesPoints < (LowerPlayer1.Rankings.DoublesPoints + LowerPlayer2.Rankings.DoublesPoints - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
        public void MixCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2)
        {
            if (UpperPlayer1.Rankings.MixPoints + UpperPlayer2.Rankings.MixPoints < (LowerPlayer1.Rankings.MixPoints + LowerPlayer2.Rankings.MixPoints - 100))
            {
                RuleBreaks.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBreaks.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}
