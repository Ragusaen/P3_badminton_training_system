using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model.Rules
{
    interface IRule
    {
        string ErrorMessage { get; set; }
        List<RuleBreak> RuleBreaks { get; set; }

        List<RuleBreak> Rule(Lineup lineup);
    }
}
