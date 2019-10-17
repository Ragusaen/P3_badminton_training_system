using MySql.Data.MySqlClient;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    class TrainerDAO : IDAO<Trainer>
    {
        public IEnumerable<Trainer> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Trainer ReadSingle(string PrimaryKeyValue)
        {
            throw new NotImplementedException();
        }

        public void WriteMany(IEnumerable<Trainer> t)
        {
            throw new NotImplementedException();
        }

        public void WriteSingle(Trainer t)
        {
            throw new NotImplementedException();
        }
    }
}
