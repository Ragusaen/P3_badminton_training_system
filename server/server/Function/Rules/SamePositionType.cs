using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.Function.Rules
{
    class SamePositionType : IRule
    {
        public int Priority { get; set; } = 5;
        private List<RuleBreak> _ruleBreaks = new List<RuleBreak>();

        public List<RuleBreak> Rule(TeamMatch match)
        {
            _ruleBreaks = new List<RuleBreak>();

            //TODO: FIX
            throw new NotImplementedException();

            return _ruleBreaks;
        }
    }
}
