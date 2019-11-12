﻿using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class SexRule : IRule
    {
        public int Priority { get; set; }

        public List<RuleBreak> Rule(Match match)
        {
            List<RuleBreak> ruleBreaks = new List<RuleBreak>();
            foreach (var position in match.Lineup.Positions)
            {
                Lineup.PositionType type = position.Key.Item1;
                if (!SexGood(type, position.Value.Player.Sex))
                {   
                    ruleBreaks.Add(new RuleBreak(position.Key, 0, "Wrong gender"));
                }

                if (position.Value is DoublePosition dp)
                {
                    if (type == Lineup.PositionType.MixDouble && position.Value.Player.Sex == dp.OtherPlayer.Sex)
                    {
                        ruleBreaks.Add(new RuleBreak(position.Key, 0, "Wrong gender"));
                        ruleBreaks.Add(new RuleBreak(position.Key, 1, "Wrong gender"));
                    }
                    else if (!SexGood(type, dp.OtherPlayer.Sex))
                    {
                        ruleBreaks.Add(new RuleBreak(position.Key, 1, "Wrong gender"));
                    }
                }
            }
            return ruleBreaks;
        }

        private bool SexGood(Lineup.PositionType position, Sex sex)
        {
            switch (position)
            {
                case Lineup.PositionType.MixDouble:
                    return true;
                case Lineup.PositionType.MensSingle:
                case Lineup.PositionType.MensDouble:
                    return sex == Sex.Male;
                case Lineup.PositionType.WomensSingle:
                case Lineup.PositionType.WomensDouble:
                    return sex == Sex.Female;
                default:
                    return false;
            }
        }
    }
}