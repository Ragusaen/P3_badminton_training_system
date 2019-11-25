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
            var db = new DatabaseEntities();
            var res = new PracticeTeam
            {
                Name = pt.Name,
                Id = pt.ID,
            };

            if (pt.TrainerID != null)
            {
                var dbTrainer = db.members.Find(pt.TrainerID);
                if (dbTrainer != null)
                    res.Trainer = (Common.Model.Trainer)dbTrainer;
            }

            return res;
        }
    }
}
