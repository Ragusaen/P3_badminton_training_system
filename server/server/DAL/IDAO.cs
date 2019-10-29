using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL
{
    interface IDAO<T>
    {
        void WriteSingle(T t);

        void WriteMany(IEnumerable<T> t);

        T ReadSingle(string PrimaryKeyValue);

        IEnumerable<T> ReadAll();
    }
}
