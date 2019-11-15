using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Common.Serialization
{
    [DataContract]
    public class CreateAccountRequest
    {
        [DataMember] public string Username;
        [DataMember] public string Password;
    }

    [DataContract]
    public class CreateAccountResponse
    {
        [DataMember] public bool WasSuccessful;
    }
 
}