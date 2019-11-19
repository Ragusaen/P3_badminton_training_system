using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class SetPlayerRequest : Request
    {
        public Player Player;
    }

    public class SetPlayerResponse : Response
    {
        public bool WasSuccessful;
    }
}
