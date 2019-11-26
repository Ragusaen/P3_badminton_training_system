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
            var res = new PracticeTeam
            {
                Name = pt.Name,
                Id = pt.ID,
                Trainer = pt.trainer == null ? null : (Common.Model.Trainer) pt.trainer
            };

            return res;
        }
    }
}
