using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    static class LeagueRules
    {

        public static readonly Dictionary<TeamMatch.Leagues, List<IRule>> Dict = new Dictionary<TeamMatch.Leagues, List<IRule>>()
        {
            { TeamMatch.Leagues.BadmintonLeague, new List<IRule>() {new LineupPointsRule(50, 100), new MinAgeRule(PlayerRanking.AgeGroup.U17), new SexRule(), new MaxPlayerOccurrencesRule(2)}},
            { TeamMatch.Leagues.Division1, new List<IRule>()},
            { TeamMatch.Leagues.Division2, new List<IRule>()},
            { TeamMatch.Leagues.Division3, new List<IRule>()},
            { TeamMatch.Leagues.DenmarksSeries, new List<IRule>()},
            { TeamMatch.Leagues.RegionalSeriesNordjylland, new List<IRule>()},
            { TeamMatch.Leagues.Series1Nordjylland, new List<IRule>()},
            { TeamMatch.Leagues.Series2Nordjylland, new List<IRule>()},
        };
    }
}