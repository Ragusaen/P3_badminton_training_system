using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class DBResetter
    {
        [TestInitialize()]
        public void ResetDatabase()
        {
            (new DBConnection()).ExecuteInsertUpdateDeleteQuery("CALL SP_UNIT_TEST_READY;", new MySql.Data.MySqlClient.MySqlParameter[0]);
        } 
    }
}
