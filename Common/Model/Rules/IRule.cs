using System.Collections.Generic;

namespace Common.Model.Rules
{
    interface IRule
    {
        string ErrorMessage { get; set; }
        List<RuleBreak> RuleBreaks { get; set; }

        List<RuleBreak> Rule(Lineup lineup);
    }
}
