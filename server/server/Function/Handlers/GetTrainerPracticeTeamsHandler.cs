using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
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
