using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetPlayerFocusPointsHandler :MiddleRequestHandler<GetPlayerFocusPointsRequest, GetPlayerFocusPointsResponse>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        protected override GetPlayerFocusPointsResponse InnerHandle(GetPlayerFocusPointsRequest request, member requester)
        {
            var db = new DatabaseEntities();

            var member = db.members.Single(p => p.ID == request.MemberId);

            var output = new GetPlayerFocusPointsResponse
            {
                FocusPoints = member.focuspoints
                    .Select(p => (Common.Model.FocusPointDescriptor)p)
                    .ToList()
            };

            output.FocusPoints.ForEach(p => 
                _log.Debug($"New focus point - Player: {member.Name} received: {p.Name}"));

            return output;
        }
    }
}
