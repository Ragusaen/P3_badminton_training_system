using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
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
