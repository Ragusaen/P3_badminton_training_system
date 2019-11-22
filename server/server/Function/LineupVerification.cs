using Common.Model;
using Server.Function.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Function
{
    class LineupVerification
    {
        public List<RuleBreak> VerifyLineup(TeamMatch match)
        {
            List<IRule> rules = LeagueRules.Dict[match.League];
            rules.Sort((p, q) => p.Priority.CompareTo(q.Priority));

            List<RuleBreak> ruleBreak = new List<RuleBreak>();
            
            foreach (IRule rule in rules)
            {
                ruleBreak = rule.Rule(match);
                if (ruleBreak.Count > 0) break;
            }
            return ruleBreak;
        }
    }
}