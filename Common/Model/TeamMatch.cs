using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Common.Model
{
    [XmlInclude(typeof(PlaySession))]
    public class TeamMatch : PlaySession
    {
        public enum Leagues
        {
            BadmintonLeague,
            Division1,
            Division2,
            Division3,
            DenmarksSeries,
            RegionalSeriesNordjylland,
            Series1Nordjylland,
            Series2Nordjylland
        }

        public int Season { get; set; }
        public Leagues League { get; set; }
        public int LeagueRound { get; set; }
        public string OpponentName { get; set; }
        public Member Captain { get; set; }
        public Lineup Lineup { get; set; }
        public int TeamIndex { get; set; }
    }
}
