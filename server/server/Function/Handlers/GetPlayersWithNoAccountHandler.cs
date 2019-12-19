using System;
using System.Linq;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class GetPlayersWithNoAccountHandler : MiddleRequestHandler<GetPlayersWithNoAccountRequest, GetPlayersWithNoAccountResponse>
    {
        protected override GetPlayersWithNoAccountResponse InnerHandle(GetPlayersWithNoAccountRequest request, member requester)
        {
            var db = new DatabaseEntities();
            var result = new GetPlayersWithNoAccountResponse();

            result.Players = db.members.Where(p => p.OnRankList && p.account == null).ToList()
                                    .Select(p => (Common.Model.Player)p).ToList();

            Console.WriteLine($"Members with no account: {db.members.Count(p => p.account == null)}");

            return result;
        }
    }
}
