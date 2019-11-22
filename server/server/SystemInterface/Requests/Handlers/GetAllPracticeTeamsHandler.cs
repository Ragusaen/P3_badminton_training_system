using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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
