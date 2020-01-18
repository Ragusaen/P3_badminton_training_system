using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
