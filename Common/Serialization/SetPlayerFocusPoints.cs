using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class SetPlayerFocusPointsRequest : Request
    {
        public Player Player;
        public List<FocusPointItem> FocusPoints;
    }

    public class SetPlayerFocusPointsResponse : Response
    {
        public bool WasSuccessful;
    }
}
