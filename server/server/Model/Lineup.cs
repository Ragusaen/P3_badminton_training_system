using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace server.Model
{
    class Lineup
    {
        public Match Match { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public int League { get; set; }
        public List<RuleBreak> RuleBakes { get; set; } = new List<RuleBreak>();
        public List<Player> MensSingle { get; set; } = new List<Player>();
        public List<Player> WomensSingle { get; set; } = new List<Player>();
        public List<Player> MensDouble { get; set; } = new List<Player>();
        public List<Player> WomensDouble { get; set; } = new List<Player>();
        public List<Player> MixDouble { get; set; } = new List<Player>();

        public List<RuleBreak> LegalCheck(List<Player> BannedPlayers, List<Lineup> ThisRound, Lineup LineupTry)
        {
            CheckSex(LineupTry);
            CheckBannedPlayers(BannedPlayers, LineupTry);
            ThisRound.Add(LineupTry);
            ThisRound = ThisRound.OrderBy(x => x.League).ToList();
            CheckTeamRanking(ThisRound[ThisRound.IndexOf(LineupTry)], ThisRound[ThisRound.IndexOf(LineupTry) + 1]);
            CheckTeamRanking(ThisRound[ThisRound.IndexOf(LineupTry) - 1], ThisRound[ThisRound.IndexOf(LineupTry)]);
            LineupCheck(LineupTry);
            return RuleBakes;
        }
        public void CheckSex(Lineup lineup)
        {
            CheckSameSex(lineup.MensSingle, Convert.ToByte("M"));
            CheckSameSex(lineup.WomensSingle, Convert.ToByte("W"));
            CheckSameSex(lineup.MensDouble, Convert.ToByte("M"));
            CheckSameSex(lineup.WomensDouble, Convert.ToByte("W"));
            CheckMixSex(lineup.MixDouble);
        }

        public void CheckSameSex(List<Player> List, byte sex)
        {
            foreach (Player Player in List)
            {
                if (Player.Member.Sex != sex)
                    RuleBakes.Add(new RuleBreak(Player, "is not the correct sex"));
            }
        }

        public void CheckMixSex(List<Player> List)
        {
            for (int i = 0; i < List.Count - 1; i++)
            {
                if (i % 2 == 1 && List[i].Member.Sex != Convert.ToByte("M"))
                    RuleBakes.Add(new RuleBreak(List[i], "is not the correct sex"));
                else if (i % 2 == 0 && List[i].Member.Sex != Convert.ToByte("W"))
                    RuleBakes.Add(new RuleBreak(List[i], "is not the correct sex"));
            }
        }

        public void CheckBannedPlayers(List<Player> BannedPlayers, Lineup lineup)
        {
            CheckDublicatePlayers(BannedPlayers, lineup.MensSingle);
            CheckDublicatePlayers(BannedPlayers, lineup.MensDouble);
            CheckDublicatePlayers(BannedPlayers, lineup.WomensDouble);
            CheckDublicatePlayers(BannedPlayers, lineup.WomensSingle);
            CheckDublicatePlayers(BannedPlayers, lineup.MixDouble);
        }
        public void CheckDublicatePlayers(List<Player> BannedPlayers, List<Player> List)
        {
            foreach (Player Player in List)
            {
                foreach (Player ID in BannedPlayers)
                {
                    if (Player.BadmintonId == ID.BadmintonId)
                    {
                        RuleBakes.Add(new RuleBreak(Player, "is not allowed to play this round"));
                    }
                }
            }
        }
        public void CheckTeamRanking(Lineup UpperLineup, Lineup LowerLineup)
        {
            SingleCheck(UpperLineup.MensSingle.Last(), LowerLineup.MensSingle.First());
            SingleCheck(UpperLineup.WomensSingle.Last(), LowerLineup.WomensSingle.First());

            DoubleCheck(UpperLineup.MensDouble.Last(), UpperLineup.MensDouble.Last(), LowerLineup.MensDouble.First(), LowerLineup.MensDouble.First());
            DoubleCheck(UpperLineup.WomensDouble.Last(), UpperLineup.WomensDouble.Last(), LowerLineup.WomensDouble.First(), LowerLineup.WomensDouble.First());
            DoubleCheck(UpperLineup.MixDouble.Last(), UpperLineup.MixDouble.Last(), LowerLineup.MixDouble.First(), LowerLineup.MixDouble.First());

        }
        public void LineupCheck(Lineup lineup)
        {
            LineupSingleCheck(lineup.MensSingle);
            LineupSingleCheck(lineup.WomensSingle);
            LineupDoubleCheck(lineup.MensDouble);
            LineupDoubleCheck(lineup.WomensDouble);
            LineupDoubleCheck(lineup.MixDouble);
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
                RuleBakes.Add(new RuleBreak(UpperPlayer, "Lower player has to many points"));
                RuleBakes.Add(new RuleBreak(LowerPlayer, "Upper player has to few points"));
            }
        }
        public void DoubleCheck(Player UpperPlayer1, Player UpperPlayer2, Player LowerPlayer1, Player LowerPlayer2)
        {
            if (UpperPlayer1.RankingDouble.Points + UpperPlayer2.RankingDouble.Points < (LowerPlayer1.RankingDouble.Points + LowerPlayer2.RankingDouble.Points - 100))
            {
                RuleBakes.Add(new RuleBreak(UpperPlayer1, "Lower player has to many points"));
                RuleBakes.Add(new RuleBreak(UpperPlayer2, "Lower player has to many points"));
                RuleBakes.Add(new RuleBreak(LowerPlayer1, "Upper player has to few points"));
                RuleBakes.Add(new RuleBreak(LowerPlayer2, "Upper player has to few points"));
            }
        }
    }
}