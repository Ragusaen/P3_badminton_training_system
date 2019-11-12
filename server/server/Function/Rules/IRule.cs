using Common.Model;
using System.Collections.Generic;

namespace Server.Function.Rules
{
    interface IRule
    {
        int Priority { get; set; }

        List<RuleBreak> Rule(Match match);
    }
}