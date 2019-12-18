using Common.Model;
using System;
using System.Collections.Generic;
using Common;

namespace Server.Function.Rules
{
    class SexRule : IRule
    {
        public int Priority { get; set; } = 2;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Verify(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();
            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    if (group.Type == Lineup.PositionType.MixDouble)
                    {
                        CheckMixSex(pos, i);
                    }
                    else
                    {
                        if(pos.Player != null && !SexGood(group.Type, pos.Player.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 0, "Player sex does not match position!"));
                        if(Lineup.PositionType.Double.HasFlag(group.Type) && pos.OtherPlayer != null && !SexGood(group.Type, pos.OtherPlayer.Sex))
                            _ruleBreaks.Add(new RuleBreak((group.Type, i), 1, "Player sex does not match position!"));
                    }
                }
            }
            return _ruleBreaks;
        }

        private void CheckMixSex(Position mix, int index)
        {
            if (mix.Player != null && mix.OtherPlayer != null)
            {
                if (!(mix.Player.Sex == Sex.Male && mix.OtherPlayer.Sex == Sex.Female) && !(mix.Player.Sex == Sex.Female && mix.OtherPlayer.Sex == Sex.Male))
                {
                    _ruleBreaks.Add(new RuleBreak((Lineup.PositionType.MixDouble, index), 0, "Mix double must have a male and a female player!"));
                    _ruleBreaks.Add(new RuleBreak((Lineup.PositionType.MixDouble, index), 1, "Mix double must have a male and a female player!"));
                }
            }
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