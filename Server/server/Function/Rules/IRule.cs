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
        // Smaller value is higher priority
        int Priority { get; set; }

        List<RuleBreak> Verify(TeamMatch match);
    }
}