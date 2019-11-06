using System;
using System.Collections.Generic;
using System.Data;

namespace Common.Model.Rules
{
    class ReservesRule : IRule
    {
        public string ErrorMessage { get; set; }
        public List<RuleBreak> RuleBreaks { get; set; }

        public List<RuleBreak> Rule(Lineup lineup)
        {
            throw new NotImplementedException();
        }
        public void CheckPlayer(Lineup lineup, List<int> memberids, int number)
        {
            throw new NotImplementedException();
        }
    }
}
