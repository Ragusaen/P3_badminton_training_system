using Common.Model;
using System;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    class SexRule : IRule
    {
        public int Priority { get; set; }
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.positions.Count; i++)
                {
                    if (group.type == Lineup.PositionType.MixDouble)
                    {
                        CheckMixSex();
                    }
                    else
                    {
                        if(!SexGood(group.type, group.positions[i].Player.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.type, i), 0, "Player sex does not match position!"));
                        if(Lineup.PositionType.Double.HasFlag(group.type) && !SexGood(group.type, group.positions[i].OtherPlayer.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.type, i), 1, "Player sex does not match position!"));
                    }
                }
            }
            return _ruleBreaks;
        }

        private void CheckMixSex()
        {
            throw new NotImplementedException();
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