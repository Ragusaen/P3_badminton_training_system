using System.Collections.Generic;
using Common;
using Common.Model;

namespace server.Function.Rules
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