using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class DataTableComparer : IEqualityComparer<DataTable>
    {
        public bool Equals(DataTable x, DataTable y)
        {
            Assert.AreEqual(x.Rows.Count, y.Rows.Count);

            return false;
        }

        public int GetHashCode(DataTable obj)
        {
            if (obj.Rows.Count != 0)
            {
                return obj.Columns.Count ^ obj.Rows.Count * obj.Columns.Count;
            }
            return 0;
        }
    }
}
