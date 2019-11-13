using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetScheduleHandler : MiddleRequestHandler<GetScheduleRequest, GetScheduleResponse>
    {
        protected override GetScheduleResponse InnerHandle(GetScheduleRequest request)
        {
            var db = new DatabaseEntities();

            var s = db.playsessions.Where(ps => ps.StartDate >= request.StartDate && ps.StartDate <= request.EndDate);
            
            GetScheduleResponse response = new GetScheduleResponse()
            {
                Matches = new List<Match>(),
                PracticeSessions = new List<PracticeSession>()
            };

            foreach (var DBps in s)
            {
                var ps = (PlaySession) DBps;
                if (ps is Match m)
                    response.Matches.Add(m);
                else if (ps is PracticeSession q)
                    response.PracticeSessions.Add(q);
            }

            return response;
        }
    }
}
