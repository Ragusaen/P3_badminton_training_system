using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;
using Server.Controller;

namespace Server.SystemInterface.Requests.Handlers
{
    class LoginHandler : MiddleRequestHandler<LoginRequest, LoginResponse>
    {
        protected override LoginResponse InnerHandle(LoginRequest request)
        {
            UserManager userManager = new UserManager();
            var response = new LoginResponse();

            // Attempt to login
            response.Token = userManager.Login(request.Username, request.Password);
            response.LoginSuccessful = response.Token.Length != 0;

            return response;
        }
    }
}
