using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.Controller;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class CreateAccountHandler : MiddleRequestHandler<CreateAccountRequest, CreateAccountResponse>
    {
        protected override CreateAccountResponse InnerHandle(CreateAccountRequest request, member requester)
        {
            UserManager userManager = new UserManager();

            if (!userManager.Create(request.Username, request.Password))
            {
                return new CreateAccountResponse()
                {
                    WasSuccessful = false
                };
            }

            var db = new DatabaseEntities();
            if (request.AddAsPlayer)
            {
                var player = db.members.Single(m => m.BadmintonPlayerID == request.BadmintonPlayerId);
                player.account = db.accounts.Find(request.Username);
            }
            else
            {
                var member = new member()
                {
                    account = db.accounts.Find(request.Username),
                    MemberType = (int)MemberType.None
                };
            }
        }
    }
}
