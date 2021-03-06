﻿using System.Collections.Generic;
using Common;
using Common.Model;

namespace server.Function.Rules
{
    class BelowEmptyPositionRule : IRule
    {
        public int Priority { get; set; } = 4;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();

            foreach (var group in match.Lineup)
            {
                for (int i = 0; i < group.Positions.Count; i++)
                {
                    var pos = group.Positions[i];
                    
                    //If a position is empty, check if below positions are empty.
                    //If false, rulebreaks are added and continue to next positiontype.
                    if (CheckPositionNull(pos, group.Type))
                        if (!CheckBelowPositions(group, i))
                            break;
                }
            }
            return _ruleBreaks;
        }

        //Checks if a position is empty
        private bool CheckPositionNull(Position pos, Lineup.PositionType type)
        {
            if (pos.Player == null)
                return true;
            if (Lineup.PositionType.Double.HasFlag(type) && pos.OtherPlayer == null)
                return true;
            return false;
        }

        //Check if any position under a certain position is not empty.
        //If so, add rulebreak.
        private bool CheckBelowPositions(Lineup.Group group, int i)
        {
            bool wasSuccessful = true;

            for (int j = i; j < group.Positions.Count; j++)
            {
                if (!CheckPositionNull(group.Positions[j], group.Type))
                {
                    wasSuccessful = false;
                    _ruleBreaks.Add(new RuleBreak((group.Type, j), 0, "Position can not be placed below an empty position!"));
                    if(Lineup.PositionType.Double.HasFlag(group.Type))
                        _ruleBreaks.Add(new RuleBreak((group.Type, j), 1, "Position can not be placed below an empty position!"));
                }
            }
            return wasSuccessful;
        }
    }
}
