using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPlayerFocusPointsRequest : Request
    {
        public Player Player;
        public List<FocusPointItem> FocusPoints;
    }

    [Serializable, XmlRoot]
    public class SetPlayerFocusPointsResponse : Response
    {
        public bool WasSuccessful;
    }
}
