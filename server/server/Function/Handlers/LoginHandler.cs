using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class LoginHandler : MiddleRequestHandler<LoginRequest, LoginResponse>
    {
        protected override LoginResponse InnerHandle(LoginRequest request, member requester)
        {
            AccountManager userManager = new AccountManager();
            var response = new LoginResponse();

            // Attempt to login
            response.Token = userManager.Login(request.Username, request.Password);
            response.LoginSuccessful = response.Token.Length != 0;

            return response;
        }
    }
}
