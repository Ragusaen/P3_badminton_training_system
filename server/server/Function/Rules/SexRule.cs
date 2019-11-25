using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class SexRule : IRule
    {
        public int Priority { get; set; } = 5;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    if (group.Type == Lineup.PositionType.MixDouble)
                    {
                        CheckMixSex();
                    }
                    else
                    {
                        if(group.Positions[i].Player != null && !SexGood(group.Type, group.Positions[i].Player.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Player sex does not match position!"));
                        if(Lineup.PositionType.Double.HasFlag(group.Type) && group.Positions[i].OtherPlayer != null && !SexGood(group.Type, group.Positions[i].OtherPlayer.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Player sex does not match position!"));
                    }
                }
            }
            return _ruleBreaks;
        }

        private void CheckMixSex()
        {
            //TODO: Fix this 
        }

        private bool SexGood(Lineup.PositionType positionType, Sex sex)
        {
            switch (positionType)
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