using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class LoginRequest : Request
    {
        public string Username;
        
        public string Password;
    }

    [Serializable, XmlRoot]
    public class LoginResponse : Response
    {
        public bool LoginSuccessful;
     
        public byte[] Token;
    }
}
