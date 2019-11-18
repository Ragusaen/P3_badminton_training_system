using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetPracticeTeamYearPlanHandler : MiddleRequestHandler<GetPracticeTeamYearPlanRequest, GetPracticeTeamYearPlanResponse>
    {
        protected override GetPracticeTeamYearPlanResponse InnerHandle(GetPracticeTeamYearPlanRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPracticeTeamYearPlanResponse
            {
                YearPlan = db.practiceteams.
                    Single(p => p.ID == request.PracticeTeamId).yearplansections.
                    Select(p => (Common.Model.YearPlanSection) p)
                    .ToList()
            };
        }
    }
}
