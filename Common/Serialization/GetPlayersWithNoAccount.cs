using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPlayersWithNoAccountRequest : Request
    {
        [DataMember] public int SomeData = 1;
    }

    [DataContract]
    public class GetPlayersWithNoAccountResponse : Response
    {
        [DataMember] public List<Player> Players;
    }
}
