using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
{
    class GetAllPlayersHandler : MiddleRequestHandler<GetAllPlayersRequest, GetAllPlayersResponse>
    {
        protected override GetAllPlayersResponse InnerHandle(GetAllPlayersRequest request, member requester)
        {
            var db = new DatabaseEntities();

            return new GetAllPlayersResponse()
            {
                Players = db.members.Where(p => (p.MemberType & (int) MemberType.Player) != 0).ToList().Select(p => (Player) p).ToList()
            };
        }
    }
}