using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class practiceteam
    {
        public static explicit operator Common.Model.PracticeTeam(practiceteam pt)
        {
            return new PracticeTeam
            {
                Name = pt.Name,
                Players = pt.members.Select(p => (Common.Model.Player) p).ToList()
            };
        }
    }
}
