using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Common.Model
{
    public class Lineup
    {
        public enum PositionType
        {
            MensSingle,
            WomensSingle,
            MensDouble,
            WomensDouble,
            MixDouble
        }

        public static bool IsDoublePosition(PositionType type)
        {
            switch (type)
            {
                case PositionType.MensDouble:
                case PositionType.WomensDouble:
                case PositionType.MixDouble:
                    return true;
                default:
                    return false;
            }
        }

        public Dictionary<Tuple<PositionType, int>, Position> Positions { get; set; }

        public static Dictionary<TeamMatch.Leagues, Dictionary<PositionType, int>> LeaguePositions =
            new Dictionary<TeamMatch.Leagues, Dictionary<PositionType, int>>()
            {
                { TeamMatch.Leagues.BadmintonLeague, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 2}, {PositionType.MensSingle, 2}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 1}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.Division1, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 2}, {PositionType.MensSingle, 2}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 1}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.Division2, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 3}, {PositionType.MensSingle, 4}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 2}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.Division3, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 3}, {PositionType.MensSingle, 4}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 2}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.DenmarksSeries, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 3}, {PositionType.MensSingle, 4}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 2}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.RegionalSeriesNordjylland, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 3}, {PositionType.MensSingle, 4}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 2}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.Series1Nordjylland, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 2}, {PositionType.MensSingle, 4}, {PositionType.WomensSingle, 2}, {PositionType.WomensDouble, 1}, {PositionType.MixDouble, 2}} },
                { TeamMatch.Leagues.Series2Nordjylland, new Dictionary<PositionType, int>() {{PositionType.MensDouble, 2}, {PositionType.MensSingle, 3}, {PositionType.WomensSingle, 1}, {PositionType.WomensDouble, 1}, {PositionType.MixDouble, 1}} },
            };
    }
}