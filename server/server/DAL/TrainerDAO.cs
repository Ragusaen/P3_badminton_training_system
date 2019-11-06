using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    class TrainerDAO : DAO<Trainer>
    {
        public TrainerDAO()
        {
            FieldColumnDictionary = new Dictionary<string, string>()
            {
                {"" }
            };
        }
    }
}
