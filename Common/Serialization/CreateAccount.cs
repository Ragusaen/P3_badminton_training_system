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
        [DataMember] public bool AddAsPlayer; // Decides if they should be added as player
        [DataMember] public int BadmintonPlayerId; // Only used if AddAsPlayer
        [DataMember] public string Name; // Only used if !AddAsPlayer
    }

    [DataContract]
    public class CreateAccountResponse
    {
        [DataMember] public bool WasSuccessful;
    }
 
}