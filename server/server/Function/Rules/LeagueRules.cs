using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    public enum RankingCompareType
    {
        CategoryOne,
        CategoryBoth,
        Level
    }

    public enum PlayersToCompare
    {
        SameCategories,
        SameSex
    }

    static class LeagueRules
    {

        public static readonly Dictionary<TeamMatch.Leagues, List<IRule>> Dict = new Dictionary<TeamMatch.Leagues, List<IRule>>()
        {
            { TeamMatch.Leagues.BadmintonLeague, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17}, RankingCompareType.CategoryOne, PlayersToCompare.SameSex),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.Division1, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17}, RankingCompareType.CategoryOne, PlayersToCompare.SameSex),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.Division2, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17}, RankingCompareType.Level, PlayersToCompare.SameCategories),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.Division3, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17}, RankingCompareType.Level, PlayersToCompare.SameCategories),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.DenmarksSeries, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17}, RankingCompareType.Level, PlayersToCompare.SameCategories),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.RegionalSeriesWest, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17, PlayerRanking.AgeGroup.U15}, RankingCompareType.CategoryOne, PlayersToCompare.SameCategories),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
            { TeamMatch.Leagues.Series1West, new List<IRule>()
            {
                new DoubleSamePlayerRule(),
                new BelowEmptyPositionRule(),
                new HalfEmptyLineupRule(),
                new LineupPointsRule(50, 100, new List<PlayerRanking.AgeGroup>(){PlayerRanking.AgeGroup.U17}),
                new MaxPlayerOccurrencesRule(2),
                new MinAgeRule(PlayerRanking.AgeGroup.U17),
                new MultipleLineupsPointsRule(new List<PlayerRanking.AgeGroup>() {PlayerRanking.AgeGroup.U17, PlayerRanking.AgeGroup.U15}, RankingCompareType.CategoryOne, PlayersToCompare.SameCategories),
                new ReservesRule(),
                new SamePositionType(),
                new SexRule(),
            }},
        };
    }
}