using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetMemberPracticeTeamsHandler : MiddleRequestHandler<GetMemberPracticeTeamRequest, GetMemberPracticeTeamResponse>
    {
        protected override GetMemberPracticeTeamResponse InnerHandle(GetMemberPracticeTeamRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetMemberPracticeTeamResponse
            {
                PracticeTeams = db.members.Single(p => p.ID == request.MemberId).practiceteams
                    .Select(p => (Common.Model.PracticeTeam) p).ToList()
            };
        }
    }
}
