using System.Collections.Generic;
using System.Linq;
using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetScheduleHandler : MiddleRequestHandler<GetScheduleRequest, GetScheduleResponse>
    {
        protected override GetScheduleResponse InnerHandle(GetScheduleRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var s = db.playsessions.Where(ps => ps.StartDate >= request.StartDate && ps.StartDate <= request.EndDate).ToList();

            _log.Debug($"Found {s.Count()} playsessions between {request.StartDate} and {request.EndDate}");
            
            GetScheduleResponse response = new GetScheduleResponse()
            {
                PlaySessions = new List<PlaySession>(),
                IsRelevantForMember = new List<bool>()
            };

            foreach (var DBps in s)
            {
                if ((PlaySession.Type) DBps.Type == PlaySession.Type.Practice)
                {
                    response.PlaySessions.Add((PracticeSession)db.practicesessions.Find(DBps.ID));
                    response.IsRelevantForMember.Add(IsRelevant(requester, DBps.practicesession));
                }
                else if ((PlaySession.Type) DBps.Type == PlaySession.Type.Match)
                {
                    response.PlaySessions.Add((TeamMatch)db.teammatches.Find(DBps.ID));
                    response.IsRelevantForMember.Add(IsRelevant(requester, DBps.teammatch));
                }
                else
                    _log.Debug($"Found play session with invalid type {DBps.Type}. ID: {DBps.ID}");
            }

            return response;
        }

        private bool IsRelevant(member m, practicesession ps)
        {
            return m.practiceteamsplayer.Any(p => p.ID == ps.practiceteam.ID)
                   || ps.TrainerID == m.ID;
        }

        private bool IsRelevant(member m, teammatch tm)
        {
            return tm.CaptainID == m.ID ||
                   tm.positions.Any(pos => pos.MemberID == m.ID);
        }
    }
}
