using System.Linq;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetPlayerFocusPointsHandler :MiddleRequestHandler<GetPlayerFocusPointsRequest, GetPlayerFocusPointsResponse>
    {
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
                _log.Debug($"Player: {member.Name} has focus point : {p.Name}"));

            return output;
        }
    }
}
