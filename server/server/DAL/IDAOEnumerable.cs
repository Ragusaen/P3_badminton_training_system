using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    interface IDAOEnumerable<T>
    {
        void Write(IEnumerable<T> t);

        IEnumerable<T> Read();
    }
}
