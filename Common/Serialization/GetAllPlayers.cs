using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{

    [Serializable, XmlRoot]
    public class GetAllPlayersRequest : Request
    { }

    [Serializable, XmlRoot]
    public class GetAllPlayersResponse : Response
    {
        public List<Player> Players;
    }
}
