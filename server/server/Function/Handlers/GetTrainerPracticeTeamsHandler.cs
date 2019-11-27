using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;
using Server.SystemInterface.Requests.Handlers;

namespace Server.Function.Handlers
{
    class GetTrainerPracticeTeamsHandler : MiddleRequestHandler<GetTrainerPracticeTeamsRequest, GetTrainerPracticeTeamsResponse>
    {
        protected override GetTrainerPracticeTeamsResponse InnerHandle(GetTrainerPracticeTeamsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetTrainerPracticeTeamsResponse
            {
                PracticeTeams = db.members.Find(request.Trainer.Member.Id).practiceteamstrainer
                    .Select(p => (Common.Model.PracticeTeam) p).ToList()
            };
        }
    }
}
