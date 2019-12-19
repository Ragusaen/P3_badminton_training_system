using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPlayerHandler : MiddleRequestHandler<GetPlayerRequest, GetPlayerResponse>
    {
        protected override GetPlayerResponse InnerHandle(GetPlayerRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetPlayerResponse
            {
                Player = (Common.Model.Player) db.members.Find(request.Id)
            };
        }
    }
}
