using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.Controller;

namespace Server.SystemInterface.Requests.Handlers
{
    class CreateAccountHandler : MiddleRequestHandler<CreateAccountRequest, CreateAccountResponse>
    {
        protected override CreateAccountResponse InnerHandle(CreateAccountRequest request)
        {
            UserManager userManager = new UserManager();

            var res = new CreateAccountResponse
            {
                WasSuccessful = userManager.Create(request.Username, request.Password)
            };

            return res;
        }
    }
}
