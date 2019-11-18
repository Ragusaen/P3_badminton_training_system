using System;

namespace Common.Model
{
    public class TeamMatch : PlaySession
    {
        public enum Leagues
        {
            BadmintonLeague,
            Division1,
            Division2,
            Division3,
            DenmarksSeries,
            RegionalSeries,
            Series1,
            Series2,
            Series3,
            Series4
        }

        public int ID { get; set; }
        public int Season { get; set; }
        public Leagues League { get; set; }
        public int LeagueRound { get; set; }
        public string OpponentName { get; set; }
        public DateTime StartDate { get; set; }
        public Member Captain { get; set; }
        public Lineup Lineup { get; set; }
    }
}
