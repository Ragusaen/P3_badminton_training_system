using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.SystemInterface.Requests.Handlers;

namespace Server.DAL
{
    class GetPlayersWithNoAccountHandler : MiddleRequestHandler<GetPlayersWithNoAccountRequest, GetPlayersWithNoAccountResponse>
    {
        protected override GetPlayersWithNoAccountResponse InnerHandle(GetPlayersWithNoAccountRequest request, member requester)
        {
            var db = new DatabaseEntities();
            var result = new GetPlayersWithNoAccountResponse();

            result.Players = db.members.Where(p => p.account == null).ToList()
                                    .Select(p => (Common.Model.Player)p).ToList();

            return result;
        }
    }
}
