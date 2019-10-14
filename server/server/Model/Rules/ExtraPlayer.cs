﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Model;

namespace Server.Model.Rules
{
    class ExtraPlayer : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            int Count = 0;
            foreach (IPosition position in lineup.Positions) 
            {
                foreach (Player player in position.Player) 
                {
                    Count = 0;
                    foreach (Player player1 in position.Player)
                    {
                        if (player.Member.Id == player1.Member.Id)
                            Count++;
                    }
                    if (Count < 2)
                    {
                        ErrorMessage = "Warning player is only plaing one match";
                        RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                    }
                    else if (Count > 2) 
                    {
                        ErrorMessage = "Error player is in to many matches";
                        RuleBreaks.Add(new RuleBreak(player, ErrorMessage));
                    }
                }
            }
            return RuleBreaks;
        }
    }
}
