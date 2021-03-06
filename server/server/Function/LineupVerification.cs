﻿using System.Collections.Generic;
using Common;
using Common.Model;
using server.Function.Rules;

namespace server.Function
{
    class LineupVerification
    {
        /// <summary>
        /// This method verifies the TeamMatches lineup
        /// </summary>
        /// <returns> A list of RuleBreaks describing what is wrong</returns>
        public List<RuleBreak> VerifyLineup(TeamMatch match)
        {
            // Get the list of rules that apply for the matches league
            List<IRule> rules = LeagueRules.Dict[match.League];

            // Sort the rules based on their priority
            rules.Sort((p, q) => p.Priority.CompareTo(q.Priority));

            List<RuleBreak> ruleBreak = new List<RuleBreak>();
            
            // Check every rule on the match
            foreach (IRule rule in rules)
            {
                ruleBreak = rule.Rule(match);

                // If there were any errors, then don't check the rest of the rules
                if (ruleBreak.Count > 0) break;
            }
            return ruleBreak;
        }
    }
}