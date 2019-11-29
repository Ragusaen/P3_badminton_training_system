using System.Linq;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.Function.Handlers
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

            // Check wether the user should be added as an existing player or a new member
            var db = new DatabaseEntities();
            if (request.AddAsPlayer)
            {
                var player = db.members.Single(m => m.BadmintonPlayerID == request.BadmintonPlayerId);
                player.account = db.accounts.Find(request.Username);
                _log.Debug("Added new account as player");
            }
            else
            {
                var member = new member()
                {
                    account = db.accounts.Find(request.Username),
                    MemberType = (int)MemberType.None,
                    Name = request.Name
                };
                db.members.Add(member);
                _log.Debug("Added new account as member");
            }

            db.SaveChanges();

            return new CreateAccountResponse()
            {
                WasSuccessful = true
            };
        }
    }
}
