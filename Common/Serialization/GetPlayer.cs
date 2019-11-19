using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace Common.Serialization
{
    public class GetPlayerRequest : Request
    {
        public int Id;
    }

    public class GetPlayerResponse : Response
    {
        public Player Player;
    }
}
