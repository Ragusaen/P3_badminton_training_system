using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPlayerRequest : Request
    {
        public int Id;
    }

    [Serializable, XmlRoot]
    public class GetPlayerResponse : Response
    {
        public Player Player;
    }
}
