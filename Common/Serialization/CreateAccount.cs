using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class CreateAccountRequest : Request
    {
        public string Username;
        public string Password;
        public bool AddAsPlayer; // Decides if they should be added as player
        public int BadmintonPlayerId; // Only used if AddAsPlayer
        public string Name; // Only used if !AddAsPlayer
    }

    [DataContract]
    public class CreateAccountResponse : Response
    {
        public bool WasSuccessful;
    }
}