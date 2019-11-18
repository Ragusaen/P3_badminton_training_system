using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [DataContract]
    public class GetPlayersWithNoAccountRequest : Request
    {
    }

    [DataContract]
    public class GetPlayersWithNoAccountResponse : Response
    {
        [DataMember]
        public List<Player> Players;
    }
}
