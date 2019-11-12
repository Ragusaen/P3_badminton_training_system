using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    static class LeagueRules
    {

        public static readonly Dictionary<Match.Leagues, List<IRule>> Dict = new Dictionary<Match.Leagues, List<IRule>>()
        {
            { Match.Leagues.BadmintonLeague, new List<IRule>() },
            { Match.Leagues.Division1, new List<IRule>()},
            { Match.Leagues.Division2, new List<IRule>()},
            { Match.Leagues.Division3, new List<IRule>()},
            { Match.Leagues.DenmarksSeries, new List<IRule>()},
            { Match.Leagues.RegionalSeries, new List<IRule>()},
            { Match.Leagues.Series1, new List<IRule>()},
            { Match.Leagues.Series2, new List<IRule>()},
            { Match.Leagues.Series3, new List<IRule>()},
            { Match.Leagues.Series4, new List<IRule>()}
        };
    }
}