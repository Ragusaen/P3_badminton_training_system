using System.Runtime.Serialization;

namespace Common.Serialization
{
    public class LoginRequest : Request
    {
        public string Username;
        
        public string Password;
    }

    public class LoginResponse : Response
    {
        public bool LoginSuccessful;
     
        public byte[] Token;
    }
}
