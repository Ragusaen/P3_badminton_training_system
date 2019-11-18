using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class position
    {
        public static explicit operator Common.Model.Position(Server.DAL.position p)
        {
            var db = new DatabaseEntities();
            return new Common.Model.Position
            {
                Player = (Common.Model.Player) p.member,
                IsExtra = p.IsExtra
            };
        }
    }
}
