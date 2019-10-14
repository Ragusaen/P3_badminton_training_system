using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller.Serialization
{
    [DataContract]
    class LoginData
    {
        [DataMember]
        public string username;
        [DataMember]
        public string password;
    }

    [DataContract]
    class LoginAttempt
    {
        [DataMember]
        public bool LoginSuccessful;
        [DataMember]
        public byte[] token;
    }
}
