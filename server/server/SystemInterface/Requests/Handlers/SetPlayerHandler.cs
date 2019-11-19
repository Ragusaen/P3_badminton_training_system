using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class SetPlayerHandler : MiddleRequestHandler<SetPlayerRequest, SetPlayerResponse>
    {
        protected override SetPlayerResponse InnerHandle(SetPlayerRequest request, member requester)
        {
            var db = new DatabaseEntities();
            var response = new SetPlayerResponse();

            var dbMember = db.members.SingleOrDefault(p => p.ID == request.Player.Member.Id);
            if (dbMember == null)
            {
                response.WasSuccessful = false;
            }
            else
            {
                throw new NotImplementedException();
            }

            return response;
        }
    }
}
