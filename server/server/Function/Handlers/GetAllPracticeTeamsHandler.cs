using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetAllPracticeTeamsHandler : MiddleRequestHandler<GetAllPracticeTeamsRequest, GetAllPracticeTeamsResponse>
    {
        protected override GetAllPracticeTeamsResponse InnerHandle(GetAllPracticeTeamsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var result = db.practiceteams.ToList().Select(p => (Common.Model.PracticeTeam) p).ToList();
            _log.Debug($"Fetching all practice teams, count = {result.Count}");
            return new GetAllPracticeTeamsResponse
            {
                PracticeTeams = result
            };
        }
    }
}
