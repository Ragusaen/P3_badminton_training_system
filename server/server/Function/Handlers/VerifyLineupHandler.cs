using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;
using Server.Function;

namespace Server.SystemInterface.Requests.Handlers
{
    class VerifyLineupHandler : MiddleRequestHandler<VerifyLineupRequest, VerifyLineupResponse>
    {
        protected override VerifyLineupResponse InnerHandle(VerifyLineupRequest request, member requester)
        {
            LineupVerification lineupVerification = new LineupVerification();
            var ruleBreaks = lineupVerification.VerifyLineup(request.Match);
            return new VerifyLineupResponse() {RuleBreaks = ruleBreaks};
        }
    }
}
