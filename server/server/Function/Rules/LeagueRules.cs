using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    static class LeagueRules
    {

        public static readonly Dictionary<TeamMatch.Leagues, List<IRule>> Dict = new Dictionary<TeamMatch.Leagues, List<IRule>>()
        {
            { TeamMatch.Leagues.BadmintonLeague, new List<IRule>() },
            { TeamMatch.Leagues.Division1, new List<IRule>()},
            { TeamMatch.Leagues.Division2, new List<IRule>()},
            { TeamMatch.Leagues.Division3, new List<IRule>()},
            { TeamMatch.Leagues.DenmarksSeries, new List<IRule>()},
            { TeamMatch.Leagues.RegionalSeries, new List<IRule>()},
            { TeamMatch.Leagues.Series1, new List<IRule>()},
            { TeamMatch.Leagues.Series2, new List<IRule>()},
            { TeamMatch.Leagues.Series3, new List<IRule>()},
            { TeamMatch.Leagues.Series4, new List<IRule>()}
        };
    }
}