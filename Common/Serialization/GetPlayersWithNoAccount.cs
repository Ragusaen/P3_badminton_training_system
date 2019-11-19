using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlayersWithNoAccountRequest : Request
    {
    }

    [Serializable, XmlRoot]
    public class GetPlayersWithNoAccountResponse : Response
    {
        public List<Player> Players;
    }
}
