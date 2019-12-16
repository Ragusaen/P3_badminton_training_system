using Common.Model;
using System.Collections.Generic;
using Common;

namespace Server.Function.Rules
{
    /// <summary>
    /// This interface specifies the requirements for each rule
    /// </summary>
    interface IRule
    {
        int Priority { get; set; }

        List<RuleBreak> Rule(TeamMatch match);
    }
}