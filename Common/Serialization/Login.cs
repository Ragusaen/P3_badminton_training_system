using System.Runtime.Serialization;

namespace Common.Serialization
{
    [DataContract]
    public class LoginRequest
    {
        [DataMember]
        public string username;
        [DataMember]
        public string password;
    }

    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public bool LoginSuccessful;
        [DataMember]
        public byte[] token;
    }
}
