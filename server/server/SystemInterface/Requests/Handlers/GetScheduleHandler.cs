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
        protected override GetScheduleResponse InnerHandle(GetScheduleRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var s = db.playsessions.Where(ps => ps.StartDate >= request.StartDate && ps.StartDate <= request.EndDate);

            Console.WriteLine($"Found {s.Count()} playsessions between {request.StartDate} and {request.EndDate}");
            
            GetScheduleResponse response = new GetScheduleResponse()
            {
                Matches = new List<TeamMatch>(),
                PracticeSessions = new List<PracticeSession>()
            };

            foreach (var DBps in s)
            {
                if ((PlaySession.Type)DBps.Type == PlaySession.Type.Practice)
                    response.PracticeSessions.Add((PracticeSession)DBps);
                else if ((PlaySession.Type)DBps.Type == PlaySession.Type.Match)
                    response.Matches.Add((TeamMatch)DBps);
                else
                    Console.WriteLine("Invalid type");
            }

            return response;
        }
    }
}
